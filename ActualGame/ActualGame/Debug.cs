﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ActualGame
{
    class Debug
    {
        #region Fields
        Dictionary<String, Texture2D > allTexts;
        List<GameObject> allObjects = new List<GameObject>();
        World debug;
        QuadTreeNode node; // TODO: Properly implement QuadTree (this var is used to avoid compile errors)
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Debug instance
        /// </summary>
        /// <param name="temp">???</param>
        public Debug(Dictionary<String, Texture2D> temp)
        {
            allTexts = temp;
        }
        #endregion

        #region method
        public void InstantiateAll()
        {
            debug = new World();
            World.Current = debug;
            
            //creates a floor
            allObjects.Add(new GameObject(80, 300, 500, 64));
            allObjects[0].LoadTexture(allTexts["Floor"]);
            allObjects[0].NoClip = false;

            //creates a player
            allObjects.Add(new Player(100, 100, node));
            allObjects[1].Position = new Vector2(200, 00);
            allObjects[1].Size = new Vector2(64, 128);
            allObjects[1].LoadTexture(allTexts["PenPen"]);
            
            //creates an enemy
            allObjects.Add(new Enemy(300, 100, node, PatrolType.Standing));
            allObjects[2].Position = new Vector2(300, 0);
            allObjects[2].LoadTexture(allTexts["Floor"]);

            debug.AllObjects = allObjects;

        }
        #endregion

        #region Update
        /// <summary>
        /// Updates all GameObjects stored in the Debug object
        /// </summary>
        /// <param name="gameTime">Reference to the Update(gameTime) value</param>
        public void UpdateAll(GameTime gameTime)
        {
            foreach(GameObject go in World.Current.AllObjects)
            {
                go.Update(gameTime);
            }
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch sb)
        {
            if(World.Current != null)
            {
                World.Current.Draw(sb);
            }
            foreach (GameObject go in allObjects)
            {
                go.Draw(sb);
            }
            
        }
        #endregion
    }
}
