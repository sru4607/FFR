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
    public enum EditorMode { Tile, Remove, Enemy, Warp }

    public partial class Map : UserControl
    {
        // Fields
        private EditorMode mode;
        private Tile[,] tiles;
        private int width;
        private int height;
        private TileData selected;
        private bool mouseIsDown;
        private List<GameEvent> events;

        // Properties
        public EditorMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                if (mode == EditorMode.Tile)
                {
                    foreach (GameEvent g in events)
                        g.Hide();
                }
                else
                {
                    foreach (GameEvent g in events)
                        g.Show();
                }
            }
        }

        public Tile this[int index1, int index2]
        {
            get
            {
                return tiles[index1 , index2];
            }
        }

        public List<GameEvent> MapEvents
        {
            get { return events; }
            set { events = value; }
        }

        public int MapWidth
        {
            get { return width; }
        }

        public int MapHeight
        {
            get { return height; }
        }

        /// <summary>
        /// Gets or sets the currently selected tile (to be used in conjunction with a Palette
        /// </summary>
        public TileData Selected
        {
            set { selected = value; }
        }

        public Map()
        {
            mode = EditorMode.Tile;
            events = new List<GameEvent>();
            mouseIsDown = false;
            InitializeComponent();
        }

        // Methods

        public void CreateMap(int width = 20, int height = 15)
        {
            // Clear the previous map
            if (tiles != null)
                foreach (Tile t in tiles)
                {
                    Controls.Remove(t);
                }

            if (events != null)
                foreach (GameEvent e in events)
                {
                    Controls.Remove(e);
                }

            // Reset event list
            events = new List<GameEvent>();

            // Initialize the tile matrix
            tiles = new Tile[width, height];
            this.width = width;
            this.height = height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j<height; j++)
                {
                    Tile t = new Tile(i,j);
                    t.Location = new Point(1 + i * 32, 1 + j * 32);
                    t.Data = Tileset.Tiles[0];
                    t.MouseDown += new MouseEventHandler(TileClick);
                    t.MouseEnter += new EventHandler(TileMouseEnter);
                    t.MouseUp += new MouseEventHandler(TileMouseUp);
                    Controls.Add(t);
                    tiles[i,j] = t;
                }
            }
        }

        private void TileMouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void TileClick(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left) && mode == EditorMode.Tile)
            {
                ((Tile)sender).Capture = false;
                mouseIsDown = !mouseIsDown;
                ChangeTile((Tile)sender);
            }
            else
            {
                if (sender is Tile)
                {
                    Tile t = (Tile)sender;
                    SetEvent(t.XIndex, t.YIndex);
                }
                else
                {
                    GameEvent g = (GameEvent)sender;
                    SetEvent(g.XIndex, g.YIndex);
                }
            }
        }

        private void TileMouseEnter(object sender, EventArgs e)
        {
            if (mouseIsDown && mode == EditorMode.Tile)
                ChangeTile((Tile)sender);
        }

        private void TileMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                Tile t = (Tile)sender;
                t.Capture = false;
            }
        }

        private void ChangeTile(Tile sender)
        {
            int xIndex = sender.XIndex;
            int yIndex = sender.YIndex;

            sender.Data = selected;
            
            if (sender.Data.IsBitmask)
            {
                UpdateAll(sender);
            }

            // Attempt to update all adjacent tiles
            // Update Tile to west
            if (xIndex - 1 >= 0 && tiles[xIndex - 1, yIndex].Data.IsBitmask)
                UpdateEast(tiles[xIndex - 1, yIndex]);

            // Update Tile to east
            if (xIndex + 1 < width && tiles[xIndex + 1, yIndex].Data.IsBitmask)
                UpdateWest(tiles[xIndex + 1, yIndex]);

            // Update Tile to north
            if (yIndex - 1 >= 0 && tiles[xIndex, yIndex - 1].Data.IsBitmask)
                UpdateSouth(tiles[xIndex, yIndex - 1]);

            // Update Tile to south
            if (yIndex + 1 < height && tiles[xIndex, yIndex + 1].Data.IsBitmask)
                UpdateNorth(tiles[xIndex, yIndex + 1]);
        }

        private void UpdateAll(Tile t)
        {
            UpdateNorth(t);
            UpdateWest(t);
            UpdateEast(t);
            UpdateSouth(t);
        }

        /// <summary>
        /// Checks the tile's north, does bitwise addition if the tiles match, bitwise subtraction if they do not
        /// </summary>
        /// <param name="t">The Tile to check</param>
        private void UpdateNorth(Tile t)
        {
            if (t.YIndex - 1 >= 0)
            {
                if (tiles[t.XIndex, t.YIndex - 1].Data.IsBitmask && tiles[t.XIndex, t.YIndex - 1].Data == t.Data)
                    t.AddAdjacency(Direction.North);
                else
                    t.RemoveAdjacency(Direction.North);
            }
        }

        /// <summary>
        /// Checks the tile's south, does bitwise addition if the tiles match, bitwise subtraction if they do not
        /// </summary>
        /// <param name="t">The Tile to check</param>
        private void UpdateSouth(Tile t)
        {
            if (t.YIndex + 1 < height)
            {
                if (tiles[t.XIndex, t.YIndex + 1].Data.IsBitmask && tiles[t.XIndex, t.YIndex + 1].Data == t.Data)
                    t.AddAdjacency(Direction.South);
                else
                    t.RemoveAdjacency(Direction.South);
            }
        }

        /// <summary>
        /// Checks the tiles east, does bitwise addition if the tiles match, bitwise subtraction if they do not
        /// </summary>
        /// <param name="t">The Tile to check</param>
        private void UpdateEast(Tile t)
        {
            if (t.XIndex + 1 < width)
            {
                if (tiles[t.XIndex + 1, t.YIndex].Data.IsBitmask && tiles[t.XIndex + 1, t.YIndex].Data == t.Data)
                    t.AddAdjacency(Direction.East);
                else
                    t.RemoveAdjacency(Direction.East);
            }
        }

        /// <summary>
        /// Checks the tiles west, does bitwise addition if the tiles match, bitwise subtraction if they do not
        /// </summary>
        /// <param name="t">The Tile to check</param>
        private void UpdateWest(Tile t)
        {
            if (t.XIndex - 1 >= 0)
            {
                if (tiles[t.XIndex - 1, t.YIndex].Data.IsBitmask && tiles[t.XIndex - 1, t.YIndex].Data == t.Data)
                    t.AddAdjacency(Direction.West);
                else
                    t.RemoveAdjacency(Direction.West);
            }
        }

        private void SetEvent(int xIndex, int yIndex)
        {
            switch (mode)
            {
                case EditorMode.Remove:
                    // Store  all events that occur at the coordinates
                    List<GameEvent> eventsAtLocation = new List<GameEvent>();
                    foreach(GameEvent g in events)
                    {
                        if (g.XIndex == xIndex && g.YIndex == yIndex)
                            eventsAtLocation.Add(g);
                    }

                    // Remove all events at these coordinates

                    foreach (GameEvent g in eventsAtLocation)
                    {
                        events.Remove(g);

                        Controls.Remove(g);
                    }

                    break;
                case EditorMode.Enemy:
                    // Only one enemy can exist at a location at a time, so first check for other enemies
                    GameEvent enemyEvent = null;
                    foreach(GameEvent g in events)
                    {
                        if (g.EventType == EventType.Enemy && g.XIndex == xIndex && g.YIndex == yIndex)
                        {
                            enemyEvent = g;
                            break;
                        }
                    }

                    // Only place an enemy if one doesn't exist
                    if (enemyEvent == null)
                    {
                        enemyEvent = new GameEvent();
                        enemyEvent.EventType = EventType.Enemy;
                        enemyEvent.Location = new Point(1 + xIndex * 32 + AutoScrollPosition.X, 1 + yIndex * 32 + AutoScrollPosition.Y);
                        enemyEvent.Image = Properties.Resources.Enemy;
                        enemyEvent.MouseClick += new MouseEventHandler(TileClick);
                        enemyEvent.XIndex = xIndex;
                        enemyEvent.YIndex = yIndex;
                        events.Add(enemyEvent);
                        Controls.Add(enemyEvent);
                        enemyEvent.BringToFront();
                    }

                    break;
                case EditorMode.Warp:
                    // Only one warp can exist at a location at a time, so first check for other warps
                    GameEvent warpEvent = null;
                    foreach (GameEvent g in events)
                    {
                        if (g.EventType == EventType.Warp && g.XIndex == xIndex && g.YIndex == yIndex)
                        {
                            // Do stuff then break
                            warpEvent = g;
                            break;
                        }
                    }

                    // Either edit the current warp if it exists or create a new one
                    if (warpEvent == null)
                    {
                        WarpCreator creator = new WarpCreator();
                        creator.ShowDialog();

                        if (creator.Finished)
                        {
                            warpEvent = new GameEvent();
                            warpEvent.EventType = EventType.Warp;
                            warpEvent.WarpData = new WarpData(creator);
                            warpEvent.Location = new Point(1 + xIndex * 32 + AutoScrollPosition.X, 1 + yIndex * 32 + AutoScrollPosition.Y);
                            warpEvent.MouseClick += new MouseEventHandler(TileClick);
                            warpEvent.Image = Properties.Resources.Warp;
                            warpEvent.XIndex = xIndex;
                            warpEvent.YIndex = yIndex;
                            events.Add(warpEvent);
                            Controls.Add(warpEvent);
                            warpEvent.BringToFront();
                        }
                    }
                    else
                    {
                        warpEvent.WarpData.WarpCreator.Show();
                    }

                    break;
            }

        }

        private Point GetPosition(int xIndex, int yIndex)
        {
            return new Point(1 + xIndex * 32, 1 + yIndex * 32);
        }

        public void SetupGameEvent(GameEvent g)
        {
            g.MouseClick += new MouseEventHandler(TileClick);
        }
    }
}
