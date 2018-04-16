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
        private NewMapForm NewMap;
        private string currentFilePath;

        // Property
        public Map Map
        {
            get { return MapView; }
        }

        public MapEditor()
        {
            InitializeComponent();
            Tileset.InitiateTileset();
            TilePalette.Update();
            MapView.Selected = TilePalette.Selected;
            MapView.CreateMap();
            NewMap = new NewMapForm(this);

        }

        private void NotImplemented(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void TilePalette_Click(object sender, EventArgs e)
        {
            MapView.Mode = EditorMode.Tile;
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

            output.Write(MapView.Events.Count);

            foreach (GameEvent g in MapView.Events)
            {
                output.Write((int)g.EventType);

                switch (g.EventType)
                {
                    case EventType.Enemy:
                        output.Write(g.XIndex);
                        output.Write(g.YIndex);
                        break;
                    case EventType.Warp:
                        output.Write(g.XIndex);
                        output.Write(g.YIndex);
                        output.Write(g.WarpData.MapName);
                        output.Write(g.WarpData.XOffset);
                        output.Write(g.WarpData.YOffset);
                        break;
                }
            }


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

            MapView.CreateMap(input.ReadInt32(), input.ReadInt32());

            for (int i = 0; i<MapView.Width; i++)
            {
                for (int j = 0; j<MapView.Height; j++)
                {
                    MapView[i,j].Data = Tileset.Sources[input.ReadString()];
                    MapView[i,j].ImageIndex = input.ReadInt32();
                    input.ReadInt32(); // Depth, currently useless for reading into this program
                }
            }
            
            MapView.Events = new List<GameEvent>(); // Number of Events, not yet implemented

            for (int i = 0; i<input.ReadInt32(); i++)
            {
                GameEvent g = new GameEvent();
                g.EventType = (EventType)input.ReadInt32();
                g.XIndex = input.ReadInt32();
                g.YIndex = input.ReadInt32();

                if (g.EventType == EventType.Warp)
                {
                    WarpCreator c = new WarpCreator();
                    c.Name = input.ReadString();
                    c.XOffset = input.ReadInt32();
                    c.YOffset = input.ReadInt32();
                }
            }

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

        private void EventButton_Click(object sender, EventArgs e)
        {
            if (sender == ClearButton)
            {
                MapView.Mode = EditorMode.Remove;
            }
            else if (sender == EnemyButton)
            {
                MapView.Mode = EditorMode.Enemy;
            }
            else if (sender == WarpButton)
            {
                MapView.Mode = EditorMode.Warp;
            }
        }
    }
}
