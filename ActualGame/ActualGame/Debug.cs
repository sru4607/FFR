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
            //creates a floor
            allObjects.Add(new GameObject());
            allObjects[0].LoadTexture(allTexts["Floor"]);
            allObjects[0].X = 80;
            allObjects[0].Y = 300;
            allObjects[0].Width = 500;
            allObjects[0].Height = 64;
            allObjects[0].hitbox = new BoundingRectangle(new Point(), 0, 0);
            ((BoundingRectangle)allObjects[0].hitbox).GetRect = allObjects[0].Rect;

            //creates a player
            allObjects.Add(new Player());
            allObjects[1].LoadTexture(allTexts["PenPen"]);
            allObjects[1].X = 100;
            allObjects[1].Y = 100;
            allObjects[1].Width = 64;
            allObjects[1].Height = 128;
            ((BoundingRectangle)(allObjects[1].hitbox)).GetRect = allObjects[1].Rect;

            //creates an enemy
            allObjects.Add(new Enemy());
            allObjects[2].LoadTexture(allTexts["PenPen"]);
            allObjects[2].X = 300;
            allObjects[2].Y = 100;
            allObjects[2].Width = 64;
            allObjects[2].Height = 128;
            allObjects[2].HitBox = null;

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
