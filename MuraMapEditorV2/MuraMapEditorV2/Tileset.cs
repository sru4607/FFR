using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuraMapEditorV2
{
    public static class Tileset
    {
        // Fields
        private static TileData[] tiles;
        private static Dictionary<string, TileData> sources;

        // Properties

        public static TileData[] Tiles
        {
            get { return tiles; }
        }

        public static Dictionary<string, TileData> Sources
        {
            get
            {
                return sources;
            }
        }

        // Methods
        public static void InitiateTileset()
        {
            // Get the file names in each tile directory
            string[] bitmaskNames = Directory.GetFiles("Assets/BitmaskTiles");
            string[] tileNames = Directory.GetFiles("Assets/Tiles");

            // Initialize the data arrays and load the images in
            tiles = new TileData[bitmaskNames.Length + tileNames.Length];

            sources = new Dictionary<string, TileData>();

            for (int i = 0; i<tileNames.Length; i++)
            {
                string sourceName = tileNames[i].Split('\\')[tileNames[i].Split('\\').Length - 1];
                sourceName = sourceName.Split('.')[0];
                if (sourceName == "Default")
                    tiles[i] = new TileData(new Bitmap(tileNames[i]), sourceName, false, -1);
                else
                    tiles[i] = new TileData(new Bitmap(tileNames[i]), sourceName);

                sources.Add(sourceName, tiles[i]);
            }

            for (int i = 0; i < bitmaskNames.Length; i++)
            {
                string sourceName = bitmaskNames[i].Split('\\')[bitmaskNames[i].Split('\\').Length - 1];
                sourceName = sourceName.Split('.')[0];
                if (sourceName == "Portal")
                    tiles[i + tileNames.Length] = new TileData(new Bitmap(bitmaskNames[i]), sourceName, true, -1);
                else
                    tiles[i + tileNames.Length] = new TileData(new Bitmap(bitmaskNames[i]), sourceName, true);

                sources.Add(sourceName, tiles[i + tileNames.Length]);

            }
        }
    }
}
