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
        /* 
         * If this is causing build errors, do the following:
         * Tools > NugetPackageManager >PackageManagerConsole > Go to the bottom of the screen near the right side and press restore 
         */
        //This is for camera stuff
        #region Fields
        Camera2D mainCam;
        GameObject parent;
        #endregion

        #region Properties
        public Camera2D MainCam
        {
            get
            {
                return mainCam;
            }
        }
        #endregion

        #region Constructor
        public Display(GraphicsDevice temp)
        {
            mainCam = new Camera2D(temp);
            parent = null;
        }
        #endregion

        #region Methods
        public void MoveCamera(float x, float y)
        {
            mainCam.Position = new Vector2(x, y);
        }
        #endregion

        #region Update
        public void Update()
        {
            if (parent != null)
            {
                mainCam.Position = new Vector2(parent.X, parent.Y);
            }

        }
        #endregion




        
        
    }
}
