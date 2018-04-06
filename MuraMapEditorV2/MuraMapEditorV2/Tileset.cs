using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuraMapEditorV2
{
    public class Tileset
    {
        // Fields
        private TileData[] tiles;
        private Dictionary<string, TileData> sources;

        // Properties

        public TileData[] Tiles
        {
            get { return tiles; }
        }

        public Dictionary<string, TileData> Sources
        {
            get
            {
                return sources;
            }
        }

        // Constructor
        public Tileset()
        {
            // Get the file names in each tile directory
            string[] bitmaskNames = Directory.GetFiles("Assets/BitmaskTiles");
            string[] tileNames = Directory.GetFiles("Assets/Tiles");

            // Initialize the data arrays and load the images in
            tiles = new TileData[bitmaskNames.Length + tileNames.Length];

            sources = new Dictionary<string, TileData>();

            for (int i = 0; i<tileNames.Length; i++)
            {
                tiles[i] = new TileData(new Bitmap(tileNames[i]), tileNames[i]);
                sources.Add(tileNames[i], tiles[i]);
            }

            for (int i = 0; i < bitmaskNames.Length; i++)
            {
                tiles[i + tileNames.Length] = new TileData(new Bitmap(bitmaskNames[i]), bitmaskNames[i], true);
                sources.Add(bitmaskNames[i], tiles[i + tileNames.Length]);
            }
        }
    }
}
