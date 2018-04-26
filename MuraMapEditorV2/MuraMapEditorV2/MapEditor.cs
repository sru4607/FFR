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
        private Size originalSize;

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
            Directory.SetCurrentDirectory("Maps");

            SaveDialog.InitialDirectory = Directory.GetCurrentDirectory();
            OpenDialog.InitialDirectory = Directory.GetCurrentDirectory();
        }

        private void SaveDialog_FileOk(object sender, CancelEventArgs e)
        {
            currentFilePath = SaveDialog.FileName;

            BinaryWriter output = new BinaryWriter(File.OpenWrite(currentFilePath));
            output.Write(MapView.MapWidth);
            output.Write(MapView.MapHeight);

            for (int i = 0; i<MapView.MapWidth; i++)
            {
                for (int j = 0; j<MapView.MapHeight; j++)
                {
                    output.Write(MapView[i,j].Data.Source);
                    output.Write(MapView[i, j].ImageIndex);
                    output.Write(MapView[i,j].Data.Depth);
                }
            }

            output.Write(MapView.MapEvents.Count);

            foreach (GameEvent g in MapView.MapEvents)
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

            for (int i = 0; i<MapView.MapWidth; i++)
            {
                for (int j = 0; j<MapView.MapHeight; j++)
                {
                    // Quick fix to enable modifying maps made before code changes to fit the current format
                    string name = input.ReadString();
                    if (name.Contains('.'))
                        name = name.Split('.')[0];
                    MapView[i,j].Data = Tileset.Sources[name];
                    MapView[i,j].ImageIndex = input.ReadInt32();
                    input.ReadInt32(); // Depth, the program already knows this value for the given material
                }
            }
            
            MapView.MapEvents = new List<GameEvent>(); // Number of Events, not yet implemented

            int numEvents = input.ReadInt32();
            for (int i = 0; i<numEvents; i++)
            {
                GameEvent g = new GameEvent();
                g.EventType = (EventType)input.ReadInt32();
                g.XIndex = input.ReadInt32();
                g.YIndex = input.ReadInt32();
                g.Location = new Point(1 + g.XIndex * 32, 1 + g.YIndex * 32);
                g.Image = Properties.Resources.Enemy;
                Map.SetupGameEvent(g);

                if (g.EventType == EventType.Warp)
                {
                    WarpCreator c = new WarpCreator();
                    c.MapName = input.ReadString();
                    c.XOffset = input.ReadInt32();
                    c.YOffset = input.ReadInt32();
                    c.LoadMap();
                    g.Image = Properties.Resources.Warp;

                    WarpData warpData = new WarpData(c);

                    g.WarpData = warpData;
                }

                MapView.MapEvents.Add(g);
                MapView.Controls.Add(g);
                g.BringToFront();
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
                output.Write(MapView.MapWidth);
                output.Write(MapView.MapHeight);

                for (int i = 0; i < MapView.MapWidth; i++)
                {
                    for (int j = 0; j < MapView.MapHeight; j++)
                    {
                        output.Write(MapView[i, j].Data.Source);
                        output.Write(MapView[i, j].ImageIndex);
                        output.Write(MapView[i, j].Data.Depth);
                    }
                }

                output.Write(MapView.MapEvents.Count);

                foreach (GameEvent g in MapView.MapEvents)
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

        private void MapEditor_ResizeBegin(object sender, EventArgs e)
        {
            originalSize = Size;
        }

        private void MapEditor_ResizeEnd(object sender, EventArgs e)
        {
            if (Size.Width < 818)
                Width = 818;
            if (Size.Height < 620)
                Height = 620;

            int widthChange = Size.Width - originalSize.Width;
            int heightChange = Size.Height - originalSize.Height;

            MapView.Width += widthChange;
            MapView.Height += heightChange;

            foreach (Control c in Controls)
            {
                if (c!=MapView && c != MapLabel && c != MenuStrip)
                {
                    Point location = c.Location;
                    location.X += widthChange;
                    c.Location = location;
                }
            }
        }
    }
}
