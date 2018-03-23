using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ActualGame
{
    class World
    {
        //For reading & displaying tiles
        String name;
        int width;
        int height;
        String path;
        Tile[,] loadedTiles;

        //Creates a world with name
        public World(String name, String path)
        {
            this.name = name;
            this.path = path;
        }

        //Imports a world saved in loadedTiles with width and height all generated from a previously created binary file
        public void Import(String path)
        {
            FileStream temp = new FileStream(path, FileMode.Open);
            BinaryReader worldReader = new BinaryReader(temp);
            width = worldReader.Read();
            height = worldReader.Read();
            loadedTiles = new Tile[height, width];
            for(int i = 0; i<width; i++)
            {
                for(int j = 0; j<height; j++)
                {
                    //Read information from file 
                    loadedTiles[j, i] = new Tile();
                }
            }

        }
    }
}
