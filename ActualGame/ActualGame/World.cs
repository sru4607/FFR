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
    class World
    {
        //For reading & displaying tiles

        #region Fields
        String name;
        int width;
        int height;
        Tile[,] tiles;
        public List<GameObject> AllObjects { get; set; }
        public static World Current { get; set; }
        #endregion

        #region Properties

        #endregion

        #region Constructor
        //Creates a world with name
        public World(Dictionary<string, Texture2D> allTextures = null, String name = "", String path = "")
        {
            this.name = name;
            AllObjects = new List<GameObject>();
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
                    t.Position = new Vector2(i * 256, j * 256);
                    t.Size = new Vector2(256, 256);
                    tiles[i, j] = t;
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
                    AllObjects.Add(new Enemy(x,y,null,PatrolType.Standing));
                }
                if(type == 1)
                {
                    //Warp
                    int x = worldReader.ReadInt32();
                    int y = worldReader.ReadInt32();
                    String destination = worldReader.ReadString();
                    int xOffset = worldReader.ReadInt32();
                    int yOffset = worldReader.ReadInt32(); 
                    AllObjects.Add(new Warp());
                }

                
            }
        }

        public Vector2 WhereCanIGetTo(PhysicsObject currentObject, Vector2 original, Vector2 future, Rectangle rect)
        {
            Vector2 MovementToTry = future - original;
            Vector2 FurthestAvailableLocationSoFar = original;
            int NumberOfStepsToBreakMovementInto = (int)(MovementToTry.Length() * 2) + 1;
            bool IsDiagonalMove = MovementToTry.X != 0 && MovementToTry.Y != 0;
            Vector2 OneStep = MovementToTry / NumberOfStepsToBreakMovementInto;
            Rectangle Rect = rect;

            for (int i = 1; i <= NumberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = original + OneStep * i;
                Rectangle newBoundary = CreateRectangleAtPosition(positionToTry, Rect.Width, Rect.Height);
                if (!HasRoomForRectangle(newBoundary, currentObject)) { FurthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    if (IsDiagonalMove)
                    {
                        int stepsLeft = NumberOfStepsToBreakMovementInto - (i - 1);

                        Vector2 remainingHorizontalMovement = OneStep.X * Vector2.UnitX * stepsLeft;
                        FurthestAvailableLocationSoFar =
                            WhereCanIGetTo(currentObject, FurthestAvailableLocationSoFar, FurthestAvailableLocationSoFar + remainingHorizontalMovement, Rect);

                        Vector2 remainingVerticalMovement = OneStep.Y * Vector2.UnitY * stepsLeft;
                        FurthestAvailableLocationSoFar =
                            WhereCanIGetTo(currentObject, FurthestAvailableLocationSoFar, FurthestAvailableLocationSoFar + remainingVerticalMovement, Rect);
                    }

                }
            }
            return FurthestAvailableLocationSoFar;
        }

        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        public bool HasRoomForRectangle(Rectangle rectangleToCheck, GameObject currentObject)
        {   if (tiles != null && tiles.Length > 0)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile.Solid && (new Rectangle((int)tile.X, (int)tile.Y, (int)tile.Width, (int)tile.Height)).Intersects(rectangleToCheck))
                    {
                        return !false;
                    }
                }
            }
            foreach (GameObject obj in AllObjects)
            {
                if (!obj.NoClip && obj != currentObject && (new Rectangle((int)obj.X, (int)obj.Y, (int)obj.Width, (int)obj.Height)).Intersects(rectangleToCheck))
                {
                    return !false;
                }
            }
            return !true;
        }


        #endregion

        #region Update
        /// <summary>
        /// Updates all objects on the given map
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAll(GameTime gameTime)
        {
            foreach (GameObject g in AllObjects)
                g.Update(gameTime);
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
