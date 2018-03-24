using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuraMapEditorV2
{
    public partial class BitmaskTile : Tile
    {
        // Fields
        private BitmaskData data;
        private int imageIndex;

        // Properties
        /// <summary>
        /// Gets or sets the tile's bitmask data
        /// </summary>
        public BitmaskData Data
        {
            get { return data; }
            set { data = value; }
        }

        public int ImageIndex
        {
            get { return imageIndex; }
            set
            {
                if (0 < value || value >= data.Length)
                    throw new IndexOutOfRangeException();

                imageIndex = value;
            }
        }

        public BitmaskTile(BitmaskData data, int xIndex = -1, int yIndex = -1)
            : base(xIndex, yIndex)
        {
            this.data = data;
            imageIndex = 0;
            TileImage = data[0];

            InitializeComponent();
        }

        public void AddAdjacency(Direction d)
        {
            imageIndex |= (int)d;
            TileImage = data[imageIndex];
        }

        public void RemoveAdjacency(Direction d)
        {
            imageIndex -= imageIndex & (int)d;
            TileImage = data[imageIndex];
        }
    }
}
