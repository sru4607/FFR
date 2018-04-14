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
        List<GameObject> allObjects;
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
            allObjects = new List<GameObject>();
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
                    //Read information from file 
                    loadedTiles[j, i] = new Tile();
                }
            }

        }

        public Vector2 WhereCanIGetTo(Vector2 original, Vector2 future, Rectangle rect)
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
                if (HasRoomForRectangle(newBoundary)) { FurthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    if (IsDiagonalMove)
                    {
                        int stepsLeft = NumberOfStepsToBreakMovementInto - (i - 1);

                        Vector2 remainingHorizontalMovement = OneStep.X * Vector2.UnitX * stepsLeft;
                        FurthestAvailableLocationSoFar =
                            WhereCanIGetTo(FurthestAvailableLocationSoFar, FurthestAvailableLocationSoFar + remainingHorizontalMovement, Rect);

                        Vector2 remainingVerticalMovement = OneStep.Y * Vector2.UnitY * stepsLeft;
                        FurthestAvailableLocationSoFar =
                            WhereCanIGetTo(FurthestAvailableLocationSoFar, FurthestAvailableLocationSoFar + remainingVerticalMovement, Rect);
                    }

                }
            }
            return FurthestAvailableLocationSoFar;
        }

        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        public bool HasRoomForRectangle(Rectangle rectangleToCheck)
        {
            foreach (Tile tile in loadedTiles)
            {
                if (tile.Solid && tile.Rect.Intersects(rectangleToCheck))
                {
                    return false;
                }
            }
            foreach (GameObject obj in allObjects)
            {
                if (!obj.NoClip && obj.Rect.Intersects(rectangleToCheck))
                {
                    return false;
                }
            }
            return true;
        }


        #endregion

        #region Update

        #endregion

        #region Draw

        #endregion





    }
}
