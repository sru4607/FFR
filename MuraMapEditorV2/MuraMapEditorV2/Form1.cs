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
    public partial class Form1 : Form
    {
        // Fields
        private Tileset tileset;

        public Form1()
        {
            InitializeComponent();
            tileset = new Tileset();
            TilePalette.Update(tileset);
            MapEditor.Selected = TilePalette.Selected;
            MapEditor.CreateMap(tileset);

        }

        private void NotImplemented(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void TilePalette_Click(object sender, EventArgs e)
        {
            MapEditor.Selected = TilePalette.Selected;
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
            string fileName = SaveDialog.FileName;

            BinaryWriter output = new BinaryWriter(File.OpenWrite(fileName));
            output.Write(MapEditor.Width);
            output.Write(MapEditor.Height);

            for (int i = 0; i<MapEditor.Width; i++)
            {
                for (int j = 0; j<MapEditor.Height; j++)
                {
                    output.Write(MapEditor[i,j].Data.Source);
                    output.Write(MapEditor[i, j].ImageIndex);
                    output.Write(MapEditor[i,j].Data.Depth);
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
            string fileName = OpenDialog.FileName;

            BinaryReader input = new BinaryReader(File.OpenRead(fileName));

            MapEditor.CreateMap(tileset, input.ReadInt32(), input.ReadInt32());

            for (int i = 0; i<MapEditor.Width; i++)
            {
                for (int j = 0; j<MapEditor.Height; j++)
                {
                    MapEditor[i,j].Data = tileset.Sources[input.ReadString()];
                    MapEditor[i,j].ImageIndex = input.ReadInt32();
                    input.ReadInt32(); // Depth, currently useless for reading into this program
                }
            }

            input.ReadInt32(); // Number of Events, not yet implemented

            input.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapEditor.CreateMap(tileset);
        }
    }
}
