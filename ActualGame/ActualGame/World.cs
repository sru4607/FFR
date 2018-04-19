using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ActualGame
{
    public class World
    {
        //For reading & displaying tiles

        #region Fields
        String name;
        int width;
        int height;
        Tile[,] tiles;
        private List<Enemy> initialEnemies;
        private List<Warp> warps;
        public List<GameObject> AllObjects { get; set; }
        public static World Current { get; set; }
        public QuadTreeNode QuadTree { get; set; }
        #endregion

        #region Constructor
        //Creates a world with name
        public World(Dictionary<string, Texture2D> allTextures = null, String name = "", String path = "")
        {
            this.name = name;
            AllObjects = new List<GameObject>();
            initialEnemies = new List<Enemy>();
            warps = new List<Warp>();

            // Load the world
            if (path!= "")
                Import(allTextures, path);
        }
        #endregion

        #region Methods
        //Imports a world saved in tiles with width and height all generated from a previously created binary file
        public void Import(Dictionary<string, Texture2D> allTextures, string filePath)
        {
            FileStream temp = new FileStream(filePath, FileMode.Open);
            BinaryReader worldReader = new BinaryReader(temp);
            width = worldReader.ReadInt32();
            height = worldReader.ReadInt32();
            QuadTree = new QuadTreeNode(0,0,width*64, height*64);
            //load tiles
            tiles = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    String  source = worldReader.ReadString();
                    int index = worldReader.ReadInt32();
                    int depth = worldReader.ReadInt32();


                    // Set the texture based on the source
                    Texture2D texture;

                    if (source == "Default")
                        texture = null;
                    else
                        texture = allTextures[source + index];

                    Tile t = new Tile(texture, depth);
                    t.Position = new Vector2(i * 64, j * 64);
                    t.Size = new Vector2(64, 64);
                    tiles[i, j] = t;
                    QuadTree.AddObject(t);
                }
            }

            int events = worldReader.ReadInt32();

            for (int i = 0; i < events; i++)
            {
                int type = worldReader.ReadInt32();
                if(type == 0)
                {
                    //Enemy
                    int x = worldReader.ReadInt32();
                    int y = worldReader.ReadInt32();
                    Enemy e = new Enemy(x*64, y*64, QuadTree, PatrolType.Standing);
                    e.Texture = allTextures["PenPen"];
                    e.WalkTexture = allTextures["PenPenWalking"];
                    AllObjects.Add(e);
                    initialEnemies.Add(e.Clone(e.Texture, e.HP, QuadTree));
                    QuadTree.AddObject(e);
                }
                if(type == 1)
                {
                    //Warp
                    int x = worldReader.ReadInt32();
                    int y = worldReader.ReadInt32();
                    String destination = worldReader.ReadString();
                    int xOffset = worldReader.ReadInt32();
                    int yOffset = worldReader.ReadInt32();
                    Warp w = new Warp(x, y, destination, xOffset, yOffset, QuadTree);
                    AllObjects.Add(w);
                    warps.Add(w);
                }

                
            }
        }

        /// <summary>
        /// Resets each world to the state it was in when first loaded
        /// </summary>
        public void ResetWorld()
        {
            AllObjects.Clear();
            QuadTree = new QuadTreeNode(0, 0, width * 64, height * 64);

            foreach (Enemy e in initialEnemies)
            {
                Enemy clone = e.Clone(e.Texture, e.HP, QuadTree);
                QuadTree.AddObject(clone);
                AllObjects.Add(clone);
            }

            foreach(Warp w in warps)
            {
                AllObjects.Add(w);
            }
        }
        //Returns the position closest you can get to, between the original and future position based on other objects
        public Vector2 WhereCanIGetTo(PhysicsObject currentObject, Vector2 original, Vector2 future, Rectangle rect)
        {
            Vector2 MovementToTry = future - original;
            Vector2 FurthestAvailableLocationSoFar = original;
            int NumberOfStepsToBreakMovementInto = (int)(MovementToTry.Length() * 2) + 1;
            bool IsDiagonalMove = MovementToTry.X != 0 && MovementToTry.Y != 0;
            Vector2 OneStep = MovementToTry / NumberOfStepsToBreakMovementInto;
            Rectangle Rect = rect;
            //splits distance into steps
            for (int i = 1; i <= NumberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = original + OneStep * i;
                Rectangle newBoundary = CreateRectangleAtPosition(positionToTry, Rect.Width, Rect.Height);
                if (!HasRoomForRectangle(newBoundary, currentObject)) { FurthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    //if movement is diagonal
                    if (IsDiagonalMove)
                    {
                        int stepsLeft = NumberOfStepsToBreakMovementInto - (i - 1);
                        //break into horizontal movement
                        Vector2 remainingHorizontalMovement = OneStep.X * Vector2.UnitX * stepsLeft;
                        FurthestAvailableLocationSoFar =
                            WhereCanIGetTo(currentObject, FurthestAvailableLocationSoFar, FurthestAvailableLocationSoFar + remainingHorizontalMovement, Rect);
                        //break into vertical movement
                        Vector2 remainingVerticalMovement = OneStep.Y * Vector2.UnitY * stepsLeft;
                        FurthestAvailableLocationSoFar =
                            WhereCanIGetTo(currentObject, FurthestAvailableLocationSoFar, FurthestAvailableLocationSoFar + remainingVerticalMovement, Rect);
                    }
                    break;

                }
            }
            return FurthestAvailableLocationSoFar;
        }
        //Create a rectangle at the vector with width and height
        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        public bool HasRoomForRectangle(Rectangle rectangleToCheck, GameObject currentObject)
        {   if (tiles != null && tiles.Length > 0)
            {
                //Tile objects collision if noCLip is false, the object is not the one we are using, and they intersect
                foreach (Tile tile in Current.tiles)
                {
                    if (!tile.noClip && (new Rectangle((int)tile.X, (int)tile.Y, (int)tile.Width, (int)tile.Height)).Intersects(rectangleToCheck))
                    {
                        return true;
                    }
                }
            }
        //Game objects collision if noCLip is false, the object is not the one we are using, and they intersect
            foreach (GameObject obj in Current.AllObjects)
            {
                if (!obj.NoClip && obj != currentObject && (new Rectangle((int)obj.X, (int)obj.Y, (int)obj.Width, (int)obj.Height)).Intersects(rectangleToCheck))
                {
                    return true;
                }
            }
            return false;
        }


        #endregion

        #region Update
        /// <summary>
        /// Updates all objects on the given map
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAll(GameTime gameTime)
        {
            for(int i =0; i<AllObjects.Count;i++)
                AllObjects[i].Update(gameTime);
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch sb)
        {
            if (tiles != null)
            {
                foreach (Tile t in tiles)
                {
                    t.Draw(sb);
                }
            }
            foreach(GameObject g in AllObjects)
            {
                g.Draw(sb);
            }
        }
        #endregion





    }
}
