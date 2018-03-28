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
        #region fields
        Dictionary<String, Texture2D > allTexts;
        List<GameObject> allObjects = new List<GameObject>();
        #endregion

        #region constructor
        public Debug(Dictionary<String, Texture2D> temp)
        {
            allTexts = temp;
        }
        #endregion

        #region method
        public void InstantiateAll()
        {
           

        }
        #endregion

        #region Update
        public void UpdateAll()
        {
            
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
