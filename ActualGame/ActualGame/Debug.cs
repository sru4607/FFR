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
        Dictionary<String, Texture2D > allTexts;
        List<GameObject> allObjects = new List<GameObject>();
        public Debug(Dictionary<String, Texture2D> temp)
        {
            allTexts = temp;
        }
        public void InstantiateAll()
        {
            allObjects.Add(new GameObject());
            allObjects[0].Height = 20;
            allObjects[0].Width = 800;
            allObjects[0].Y = 400;
            allObjects[0].HitBox = new BoundingRectangle(allObjects[0].Rect.Center, allObjects[0].Rect.Width, allObjects[0].Rect.Height);
            allObjects[0].LoadTexture(allTexts["Floor"]);

            allObjects.Add(new Player());
            allObjects[1].Height = 150;
            allObjects[1].Width = 50;
            allObjects[1].Y = 200;
            allObjects[1].X = 200;
            allObjects[1].Physics = true;
            allObjects[1].LoadTexture(allTexts["PenPen"]);

        }
        public void UpdateAll()
        {
            for(int i = 0; i<allObjects.Count; i++)
            {
                allObjects[i].Update();
            }
            for(int i = 0; i<allObjects.Count; i++)
            {
                if(allObjects[i].Physics)
                    allObjects[i].Collision(allObjects);
            }
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (GameObject go in allObjects)
            {
                go.Draw(sb);
            }
            
        }
    }
}
