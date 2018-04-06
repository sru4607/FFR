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
    public partial class MapEditor : Form
    {
        // Fields
        private Tileset tileset;
        private NewMapForm NewMap;
        private string currentFilePath;

        // Property
        public Map Map
        {
            get { return MapView; }
        }

        public Tileset Tileset
        {
            get { return tileset; }
        }

        public MapEditor()
        {
            InitializeComponent();
            tileset = new Tileset();
            TilePalette.Update(tileset);
            MapView.Selected = TilePalette.Selected;
            MapView.CreateMap(tileset);
            NewMap = new NewMapForm(this);

        }

        private void NotImplemented(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void TilePalette_Click(object sender, EventArgs e)
        {
            MapView.Selected = TilePalette.Selected;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDialog.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("Maps"))
                Directory.CreateDirectory("Maps");
            SaveDialog.InitialDirectory = Directory.GetCurrentDirectory() + "/Maps";
            OpenDialog.InitialDirectory = Directory.GetCurrentDirectory() + "/Maps";
        }

        private void SaveDialog_FileOk(object sender, CancelEventArgs e)
        {
            currentFilePath = SaveDialog.FileName;

            BinaryWriter output = new BinaryWriter(File.OpenWrite(currentFilePath));
            output.Write(MapView.Width);
            output.Write(MapView.Height);

            for (int i = 0; i<MapView.Width; i++)
            {
                for (int j = 0; j<MapView.Height; j++)
                {
                    output.Write(MapView[i,j].Data.Source);
                    output.Write(MapView[i, j].ImageIndex);
                    output.Write(MapView[i,j].Data.Depth);
                }
            }

            output.Write(0);


            output.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDialog.ShowDialog();
        }

        private void OpenDialog_FileOk(object sender, CancelEventArgs e)
        {
            currentFilePath = OpenDialog.FileName;

            BinaryReader input = new BinaryReader(File.OpenRead(currentFilePath));

            MapView.CreateMap(tileset, input.ReadInt32(), input.ReadInt32());

            for (int i = 0; i<MapView.Width; i++)
            {
                for (int j = 0; j<MapView.Height; j++)
                {
                    MapView[i,j].Data = tileset.Sources[input.ReadString()];
                    MapView[i,j].ImageIndex = input.ReadInt32();
                    input.ReadInt32(); // Depth, currently useless for reading into this program
                }
            }

            input.ReadInt32(); // Number of Events, not yet implemented

            input.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMap.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFilePath != null)
            {
                BinaryWriter output = new BinaryWriter(File.OpenWrite(currentFilePath));
                output.Write(MapView.Width);
                output.Write(MapView.Height);

                for (int i = 0; i < MapView.Width; i++)
                {
                    for (int j = 0; j < MapView.Height; j++)
                    {
                        output.Write(MapView[i, j].Data.Source);
                        output.Write(MapView[i, j].ImageIndex);
                        output.Write(MapView[i, j].Data.Depth);
                    }
                }

                output.Write(0);


                output.Close();
            }
            else
            {
                SaveDialog.ShowDialog();
            }
        }
    }
}
