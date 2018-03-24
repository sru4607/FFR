using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MuraMapEditorV2
{
    public enum Direction { North = 1, West = 2, East = 4, South = 8 }

    public class TileData
    {
        // Fields
        private Bitmap[] tileOrientations;
        private string source;
        private int depth;

        // Properties
        public Bitmap this[int index]
        {
            get
            {
                if (index < 0 || index >= tileOrientations.Length)
                    throw new IndexOutOfRangeException();

                return tileOrientations[index];
            }
        }

        public string Source
        {
            get
            {
                return source;
            }
        }

        public int Depth
        {
            get
            {
                return depth;
            }
        }

        public int Length
        {
            get { return tileOrientations.Length; }
        }

        public bool IsBitmask
        {
            get { return tileOrientations.Length > 1; }
        }

        // Constructor
        public TileData(Bitmap source, string sourceName, bool isBitmask = false)
        {
            this.source = sourceName;
            depth = 0;
            if (isBitmask)
            {
                #region BitmaskAlgorithm
                Bitmap[] miniTiles = new Bitmap[9];

                // Break the source image up into 16x16 pieces
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        miniTiles[i + 3 * j] = source.Clone(new Rectangle(i * 16, j * 16, 16, 16), source.PixelFormat);
                    }
                }

                // Reassemble the pieces into all possible shapes of the bitmasked tile
                tileOrientations = new Bitmap[16];
                for (int i = 0; i < 16; i++)
                {
                    tileOrientations[i] = new Bitmap(32, 32);
                    Graphics tileFormer = Graphics.FromImage(tileOrientations[i]);

                    // Northwest corner
                    switch (i & 3)
                    {
                        case 0:
                            tileFormer.DrawImage(miniTiles[0], 0, 0);
                            break;
                        case 1:
                            tileFormer.DrawImage(miniTiles[3], 0, 0);
                            break;
                        case 2:
                            tileFormer.DrawImage(miniTiles[1], 0, 0);
                            break;
                        case 3:
                            tileFormer.DrawImage(miniTiles[4], 0, 0);
                            break;
                    }

                    // Northeast corner
                    switch (i & 5)
                    {
                        case 0:
                            tileFormer.DrawImage(miniTiles[2], 16, 0);
                            break;
                        case 1:
                            tileFormer.DrawImage(miniTiles[5], 16, 0);
                            break;
                        case 4:
                            tileFormer.DrawImage(miniTiles[1], 16, 0);
                            break;
                        case 5:
                            tileFormer.DrawImage(miniTiles[4], 16, 0);
                            break;
                    }

                    // Southwest corner
                    switch (i & 10)
                    {
                        case 0:
                            tileFormer.DrawImage(miniTiles[6], 0, 16);
                            break;
                        case 2:
                            tileFormer.DrawImage(miniTiles[7], 0, 16);
                            break;
                        case 8:
                            tileFormer.DrawImage(miniTiles[3], 0, 16);
                            break;
                        case 10:
                            tileFormer.DrawImage(miniTiles[4], 0, 16);
                            break;
                    }

                    // Southeast corner
                    switch (i & 12)
                    {
                        case 0:
                            tileFormer.DrawImage(miniTiles[8], 16, 16);
                            break;
                        case 4:
                            tileFormer.DrawImage(miniTiles[7], 16, 16);
                            break;
                        case 8:
                            tileFormer.DrawImage(miniTiles[5], 16, 16);
                            break;
                        case 12:
                            tileFormer.DrawImage(miniTiles[4], 16, 16);
                            break;
                    }
                }
                #endregion
            }
            else
            {
                tileOrientations = new Bitmap[1];
                tileOrientations[0] = source;

            }
        }
    }
}
