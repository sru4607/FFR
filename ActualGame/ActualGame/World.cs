using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        #endregion

        #region Properties

        #endregion

        #region Constructor
        //Creates a world with name
        public World(String name = "", String path = "")
        {
            this.name = name;
            this.path = path;
            CurrentBoard = this;
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
                    //loadedTiles[j, i] = new Tile();
                }
            }

        }


        #endregion

        #region Update

        #endregion

        #region Draw

        #endregion

        public List<GameObject> Tiles { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public Texture2D TileTexture { get; set; }
        private SpriteBatch SpriteBatch { get; set; }
        private Random _rnd = new Random();
        public static World CurrentBoard { get; private set; }

        public World(SpriteBatch spritebatch, Texture2D tileTexture, int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
            TileTexture = tileTexture;
            SpriteBatch = spritebatch;
            Tiles = new List<GameObject>(Rows);
            World.CurrentBoard = this;
        }



        public void Draw(SpriteBatch sb)
        {
            foreach (var tile in Tiles)
            {
                tile.Draw(sb);
            }
        }

        public bool HasRoomForRectangle(Rectangle rectangleToCheck)
        {
            foreach (var tile in World.CurrentBoard.Tiles)
            {
                if (!tile.NoClip && tile.Rect.Intersects(rectangleToCheck))
                {
                    return false;
                }
            }
            return true;
        }

        public Vector2 WhereCanIGetTo(Vector2 originalPosition, Vector2 destination, Rectangle boundingRectangle)
        {
            MovementWrapper move = new MovementWrapper(originalPosition, destination, boundingRectangle);

            for (int i = 1; i <= move.NumberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = originalPosition + move.OneStep * i;
                Rectangle newBoundary = CreateRectangleAtPosition(positionToTry, boundingRectangle.Width, boundingRectangle.Height);
                if (HasRoomForRectangle(newBoundary)) { move.FurthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    if (move.IsDiagonalMove)
                    {
                        move.FurthestAvailableLocationSoFar = CheckPossibleNonDiagonalMovement(move, i);
                    }
                    break;
                }
            }
            return move.FurthestAvailableLocationSoFar;
        }

        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        private Vector2 CheckPossibleNonDiagonalMovement(MovementWrapper wrapper, int i)
        {
            if (wrapper.IsDiagonalMove)
            {
                int stepsLeft = wrapper.NumberOfStepsToBreakMovementInto - (i - 1);

                Vector2 remainingHorizontalMovement = wrapper.OneStep.X * Vector2.UnitX * stepsLeft;
                wrapper.FurthestAvailableLocationSoFar =
                    WhereCanIGetTo(wrapper.FurthestAvailableLocationSoFar, wrapper.FurthestAvailableLocationSoFar + remainingHorizontalMovement, wrapper.BoundingRectangle);

                Vector2 remainingVerticalMovement = wrapper.OneStep.Y * Vector2.UnitY * stepsLeft;
                wrapper.FurthestAvailableLocationSoFar =
                    WhereCanIGetTo(wrapper.FurthestAvailableLocationSoFar, wrapper.FurthestAvailableLocationSoFar + remainingVerticalMovement, wrapper.BoundingRectangle);
            }

            return wrapper.FurthestAvailableLocationSoFar;
        }



    }
}
