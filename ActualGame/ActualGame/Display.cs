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
        PhysicsObject parent;
        GraphicsDevice temp;
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
        /// <summary>
        /// Creates a display object to control camera
        /// </summary>
        /// <param name="temp">the games Graphics device from game 1</param>
        /// <param name="parent">the Player object</param>
        public Display(GraphicsDevice temp, PhysicsObject parent)
        {
            mainCam = new Camera2D(temp);
            mainCam.Position = new Vector2(parent.X - temp.DisplayMode.Width / 2, parent.Y - temp.DisplayMode.Height / 2);
            this.temp = temp;
            this.parent = parent;
        }
        #endregion

        #region Methods
        #endregion

        #region Update
        /// <summary>
        /// Updates position based on parent object movement, if parent moves in the Y the camera moves, if the parent moves more than 1225 pixels in the x away the camera follows
        /// </summary>
        public void Update()
        {

            mainCam.Position = new Vector2(mainCam.Position.X, parent.Y - temp.DisplayMode.Height / 2);
            if(Math.Abs(mainCam.Position.X - parent.X)>1225)
            {
                mainCam.Position = new Vector2(mainCam.Position.X + parent.Movement.X, mainCam.Position.Y);
            }
                

        }
        #endregion




        
        
    }
}
