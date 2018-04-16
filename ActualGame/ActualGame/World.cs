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
        String path;
        Tile[,] loadedTiles;
        public List<GameObject> AllObjects { get; set; }
        public static World Current { get; set; }
        #endregion

        #region Properties

        #endregion

        #region Constructor
        //Creates a world with name
        public World(String name = "", String path = "")
        {
            this.name = name;
            this.path = path;
            AllObjects = new List<GameObject>();
        }
        #endregion

        #region Methods
        //Imports a world saved in loadedTiles with width and height all generated from a previously created binary file
        public void Import()
        {
            FileStream temp = new FileStream(path, FileMode.Open);
            BinaryReader worldReader = new BinaryReader(temp);
            width = worldReader.Read();
            height = worldReader.Read();
            loadedTiles = new Tile[height, width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    String  source = worldReader.ReadString();
                    int index = worldReader.Read();
                    int depth = worldReader.Read();
                    loadedTiles[i, j] = new Tile();
                }
            }

            int events = worldReader.Read();

            for (int i = 0; i < events; i++)
            {
                int type = worldReader.Read();
                if(type == 0)
                {
                    //Enemy
                    int x = worldReader.Read();
                    int y = worldReader.Read();
                    AllObjects.Add(new Enemy(x,y,null,PatrolType.Standing));
                }
                if(type == 1)
                {
                    //Warp
                    int x = worldReader.Read();
                    int y = worldReader.Read();
                    String warpTo = worldReader.ReadString();
                    int xOff = worldReader.Read();
                    int yOff = worldReader.Read(); 
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
        {   if (loadedTiles != null && loadedTiles.Length > 0)
            {
                foreach (Tile tile in loadedTiles)
                {
                    if (tile.Solid && (new Rectangle((int)tile.X, (int)tile.Y, (int)tile.Width, (int)tile.Height)).Intersects(rectangleToCheck))
                    {
                        return true;
                    }
                }
            }
            foreach (GameObject obj in AllObjects)
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

        #endregion

        #region Draw
        public void Draw(SpriteBatch sb)
        {
            if (loadedTiles != null)
            {
                foreach (Tile t in loadedTiles)
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
