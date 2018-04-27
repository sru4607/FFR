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
    public partial class Palette : UserControl
    {
        // Fields
        private PictureBox[] tileImages;
        private int selected;
        private PictureBox cursor;

        // Properties
        public TileData Selected
        {
            get { return Tileset.Tiles[selected]; }
        }

        public Palette()
        {
            InitializeComponent();

            // Initialize tileImages
            tileImages = new PictureBox[1];
            PictureBox p = new PictureBox();
            p.Location = new Point(1, 1);
            p.Size = new Size(32, 32);
            tileImages[0] = p;
            Controls.Add(p);

            selected = 0;

            //// Initialize the cursor
            //cursor = new PictureBox();
            //cursor.Image = Image.FromFile("Assets/Cursor.png");
            //cursor.Location = new Point(0, 0);
            //cursor.Width = 34;
            //cursor.Height = 34;
            //Controls.Add(cursor);
        }

        // Methods
        /// <summary>
        /// Updates the palette with the new tileset
        /// </summary>
        /// <param name="tileset">The tileset to change the palette to</param>
        public void Update()
        {
            // First remove old tiles ( the t is archaic)
            foreach (PictureBox t in tileImages)
            {
                Controls.Remove(t);
            }
            

            // Initialize the pictureboxes representing the tiles
            tileImages = new PictureBox[Tileset.Tiles.Length];

            for (int i = 0; i<Tileset.Tiles.Length; i++)
            {
                PictureBox p = new PictureBox();
                p.Location = new Point((i % 3) * 33 + 1, (i / 3) * 33 + 1);
                p.Size = new Size(32, 32);
                p.Image = Tileset.Tiles[i][0];
                tileImages[i] = p;
                p.Click += new EventHandler(PicClick);
                Controls.Add(p);
            }

            selected = 0;

            // Reset the cursor
            //cursor.Location = new Point(0, 0);
        }

        private void PicClick(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                for (int i = 0; i < tileImages.Length; i++)
                {
                    if (tileImages[i] == (PictureBox)sender)
                    {
                        selected = i;
                        break;
                    }
                }
            }

            base.OnClick(e);
        }
    }
}
