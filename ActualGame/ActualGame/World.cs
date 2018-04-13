﻿using System;
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

        #region Fields
        String name;
        int width;
        int height;
        String path;
        Tile[,] loadedTiles;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        //Creates a world with name
        public World(String name = "", String path = "")
        {
            this.name = name;
            this.path = path;
            CurrentBoard = this;
        }
        #endregion

        #region Methods
        //Imports a world saved in loadedTiles with width and height all generated from a previously created binary file
        public void Import()
        {
            FileStream temp = new FileStream(path, FileMode.Open);
            BinaryReader worldReader = new BinaryReader(temp);
            width = worldReader.Read();
            height = worldReader.Read();
            loadedTiles = new Tile[height, width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //Read information from file 
                    loadedTiles[j, i] = new Tile();
                }
            }

        }
        #endregion

        #region Update

        #endregion

        #region Draw

        #endregion





    }
}
