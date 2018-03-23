using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace ActualGame
{
    class Display
    {
        //This is for camera stuff
        Camera2D mainCam;
        GameObject parent;
        public Display(GraphicsDevice temp)
        {
            mainCam = new Camera2D(temp);
            parent = null;
        }
        public void MoveCamera(float x, float y)
        {
            mainCam.Position = new Vector2(x, y); 
        }
        public void Update()
        {
            if(parent != null)
            {
                mainCam.Position = new Vector2(parent.X, parent.Y);
            }
            
        }

        public  Camera2D MainCam
        {
            get
            {
                return mainCam;
            }
        }
        
    }
}
