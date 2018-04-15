using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmaskImageConverter
{
    public enum Direction { North = 1, West = 2, East = 4, South = 8 }

    public partial class Form1 : Form
    {
        // Field for storing the different orientations of the tile
        private Bitmap[] orientations;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenDialog.ShowDialog();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveDialog.ShowDialog();
        }

        private void OpenDialog_FileOk(object sender, CancelEventArgs e)
        {
            Bitmap source = new Bitmap(OpenDialog.FileName);

            // Check the dimensions of the source to see if they're valid
            if (source.Width != 48 || source.Height != 48)
            {
                MessageBox.Show("Error: Image dimensions must be 48x48");
                e.Cancel = true;
            }

            SaveButton.Enabled = true;

            OriginalImage.Image = source;

            // Break up the source into subsections
            Bitmap[] sections = new Bitmap[9];

            for (int i = 0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    sections[i + j * 3] = source.Clone(new Rectangle(i * 16, j * 16, 16, 16), source.PixelFormat);
                }
            }

            // Generate the orientations
            Bitmap[] tiles = new Bitmap[16];

            for (int i = 0; i<16; i++)
            {
                tiles[i] = new Bitmap(32, 32);
                Graphics tileFormer = Graphics.FromImage(tiles[i]);

                // Northwest corner
                switch (i & 3)
                {
                    case 0:
                        tileFormer.DrawImage(sections[0], 0, 0);
                        break;
                    case 1:
                        tileFormer.DrawImage(sections[3], 0, 0);
                        break;
                    case 2:
                        tileFormer.DrawImage(sections[1], 0, 0);
                        break;
                    case 3:
                        tileFormer.DrawImage(sections[4], 0, 0);
                        break;
                }

                // Northeast corner
                switch (i & 5)
                {
                    case 0:
                        tileFormer.DrawImage(sections[2], 16, 0);
                        break;
                    case 1:
                        tileFormer.DrawImage(sections[5], 16, 0);
                        break;
                    case 4:
                        tileFormer.DrawImage(sections[1], 16, 0);
                        break;
                    case 5:
                        tileFormer.DrawImage(sections[4], 16, 0);
                        break;
                }

                // Southwest corner
                switch (i & 10)
                {
                    case 0:
                        tileFormer.DrawImage(sections[6], 0, 16);
                        break;
                    case 2:
                        tileFormer.DrawImage(sections[7], 0, 16);
                        break;
                    case 8:
                        tileFormer.DrawImage(sections[3], 0, 16);
                        break;
                    case 10:
                        tileFormer.DrawImage(sections[4], 0, 16);
                        break;
                }

                // Southeast corner
                switch (i & 12)
                {
                    case 0:
                        tileFormer.DrawImage(sections[8], 16, 16);
                        break;
                    case 4:
                        tileFormer.DrawImage(sections[7], 16, 16);
                        break;
                    case 8:
                        tileFormer.DrawImage(sections[5], 16, 16);
                        break;
                    case 12:
                        tileFormer.DrawImage(sections[4], 16, 16);
                        break;
                }
            }

            orientations = tiles;

            // Create the new image
            Bitmap splice = new Bitmap(128, 128);
            Graphics g = Graphics.FromImage(splice);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    g.DrawImage(tiles[i + j * 4], i * 32, j * 32);
                }
            }

            // Set the spliced image to the splice
            SplicedImage.Image = splice;

        }

        private void SaveDialog_FileOk(object sender, CancelEventArgs e)
        {
            SplicedImage.Image.Save(SaveDialog.FileName);

            string[] fileNameArray = SaveDialog.FileName.Split('\\');
            string filePath = SaveDialog.FileName.Substring(0, SaveDialog.FileName.IndexOf(fileNameArray[fileNameArray.Length - 1]));
            string fileName = fileNameArray[fileNameArray.Length - 1].Split('.')[0];

            if (!Directory.Exists(filePath + fileName + "Orientations"))
            {
                Directory.CreateDirectory(filePath + fileName + "Orientations");
            }

            for (int i =0; i<orientations.Length; i++)
            {
                orientations[i].Save(filePath + fileName + "Orientations\\" + fileName + i + ".png");
            }
            
        }
    }
}
