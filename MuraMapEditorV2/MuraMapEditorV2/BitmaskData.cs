using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MuraMapEditorV2
{
    public class BitmaskData
    {
        // Fields
        private Bitmap[] tileShapes;

        // Properties
        public Bitmap this[int index]
        {
            get
            {
                if (index < 0 || index >= tileShapes.Length)
                    throw new IndexOutOfRangeException();

                return tileShapes[index];
            }
        }

        public int Length
        {
            get { return tileShapes.Length; }
        }

        // Constructor
        public BitmaskData(Bitmap image)
        {
            Bitmap[] miniTiles = new Bitmap[9];

            // Break the source image up into 16x16 pieces
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    miniTiles[i + 3 * j] = image.Clone(new Rectangle(i * 16, j * 16, 16, 16), image.PixelFormat);
                }
            }

            // Reassemble the pieces into all possible shapes of the bitmasked tile
            tileShapes = new Bitmap[16];
            for (int i = 0; i < 16; i++)
            {
                tileShapes[i] = new Bitmap(32, 32);
                Graphics tileFormer = Graphics.FromImage(tileShapes[i]);

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
        }
    }
}
