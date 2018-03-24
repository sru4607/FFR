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
    public partial class Map : UserControl
    {
        // Fields
        private Tile[,] tiles;
        private int width;
        private int height;
        private TileData selected;
        private bool mouseIsDown;

        // Properties
        public Tile this[int index1, int index2]
        {
            get
            {
                return tiles[index1 , index2];
            }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
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
            mouseIsDown = false;
            InitializeComponent();
        }

        // Methods

        public void CreateMap(Tileset tileset, int width = 20, int height = 15)
        {
            // Clear the previous map
            if (tiles != null)
                foreach (Tile t in tiles)
                {
                    Controls.Remove(t);
                }

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
                    t.Data = tileset.Tiles[0];
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
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                ((Tile)sender).Capture = false;
                mouseIsDown = !mouseIsDown;
                ChangeTile((Tile)sender);
            }
        }

        private void TileMouseEnter(object sender, EventArgs e)
        {
            if (mouseIsDown)
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

        private Point GetPosition(int xIndex, int yIndex)
        {
            return new Point(1 + xIndex * 32, 1 + yIndex * 32);
        }
    }
}
