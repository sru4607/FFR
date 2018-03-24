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
    public partial class Tile : UserControl
    {
        // Fields
        protected int xIndex;
        protected int yIndex;
        protected TileData data;
        protected int imageIndex;

        // Properties
        /// <summary>
        /// Gets or sets the image for the tile
        /// </summary>
        public Image TileImage
        {
            get { return ImageDisplay.Image; }
            set { ImageDisplay.Image = value; }
        }

        /// <summary>
        /// Gets or sets the data stored in the tile
        /// </summary>
        public TileData Data
        {
            get { return data; }
            set
            {
                data = value;
                imageIndex = 0;
                ImageDisplay.Image = data[imageIndex];
            }
        }

        /// <summary>
        /// Gets the xIndex of the containing matrix (-1 if not in a matrix)
        /// </summary>
        public int XIndex
        {
            get { return xIndex; }
            set { xIndex = value; }
        }

        /// <summary>
        /// Gets the yIndex of the containing matrix (-1 if not in a matrix)
        /// </summary>
        public int YIndex
        {
            get { return yIndex; }
            set { yIndex = value; }
        }

        public int ImageIndex
        {
            get { return imageIndex; }
            set
            {
                if (value >= 0 && value < data.Length)
                {
                    imageIndex = value;
                    ImageDisplay.Image = data[imageIndex];
                }
            }
        }

        public Tile(int xIndex = -1, int yIndex = -1)
        {
            InitializeComponent();
            this.xIndex = xIndex;
            this.yIndex = yIndex;
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

        #region PictureBox interaction Fixes
        private void ImageDisplay_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void ImageDisplay_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        private void ImageDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void ImageDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        private void ImageDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void ImageDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }
        #endregion
    }
}
