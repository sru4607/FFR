using System;
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
            //creates a floor
            allObjects.Add(new GameObject(80, 300, 500, 64));
            allObjects[0].LoadTexture(allTexts["Floor"]);

            //creates a player
            allObjects.Add(new Player(100, 100, node));
            allObjects[1].LoadTexture(allTexts["PenPen"]);
            
            //creates an enemy
            allObjects.Add(new Enemy(300, 100, node, PatrolType.Standing));
            allObjects[2].LoadTexture(allTexts["PenPen"]);

        }
        #endregion

        #region Update
        /// <summary>
        /// Updates all GameObjects stored in the Debug object
        /// </summary>
        /// <param name="gameTime">Reference to the Update(gameTime) value</param>
        public void UpdateAll(GameTime gameTime)
        {
            for(int i = 0; i < allObjects.Count; i++)
            {
                allObjects[i].Update();
            }
            allObjects[1].Collision(allObjects);
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch sb)
        {
            foreach (GameObject go in allObjects)
            {
                go.Draw(sb);
            }
            
        }
        #endregion
    }
}
