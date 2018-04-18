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

namespace MuraMapEditorV2
{
    public partial class WarpCreator : Form
    {
        // Fields
        private PictureBox[,] map;
        private int xOffset;
        private int yOffset;
        private bool finished;
        private string mapName;

        // Properties
        public string MapName
        {
            get { return mapName; }
            set { mapName = value; }
        }

        public bool Finished
        {
            get { return finished; }
        }

        public int XOffset
        {
            get { return xOffset; }
            set { xOffset = value; }
        }

        public int YOffset
        {
            get { return yOffset; }
            set { yOffset = value; }
        }

        public WarpCreator()
        {
            InitializeComponent();
        }

        private void DestinationButton_Click(object sender, EventArgs e)
        {
            OpenDialog.ShowDialog();
        }

        private void SetWarpButton_Click(object sender, EventArgs e)
        {
            finished = true;
            Hide();
        }

        private void OpenDialog_FileOk(object sender, CancelEventArgs e)
        {
            mapName = OpenDialog.FileName;

            xOffset = 0;
            yOffset = 0;
            LoadMap();
        }

        public void LoadMap()
        {
            try
            {
                BinaryReader input = new BinaryReader(File.OpenRead(mapName));

                map = new PictureBox[input.ReadInt32(), input.ReadInt32()];

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        PictureBox p = new PictureBox();
                        p.Size = new Size(32, 32);
                        p.Location = new Point(12 + i * 32, 40 + j * 32);
                        p.Image = Tileset.Sources[input.ReadString()][input.ReadInt32()];
                        input.ReadInt32(); // Depth
                        p.Click += new EventHandler(PictureBox_Click);
                        map[i, j] = p;
                        Controls.Add(p);
                    }
                }

                
                DestinationLabel.Text = "(x,y) = (" + xOffset + "," +yOffset + ")"
                    
                    
                    
                    
                    
                    








;

                SetWarpButton.Enabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error when loading a warp event:\n" + e.Message);
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            for (int i =0; i<map.GetLength(0); i++)
            {
                for (int j = 0; j<map.GetLength(1); j++)
                {
                    if (map[i, j] == sender)
                    {
                        xOffset = i;
                        yOffset = j;
                        DestinationLabel.Text = "(x,y) = (" + xOffset + "," + yOffset + ")";
                        break;
                    }
                }
            }
        }
    }
}
