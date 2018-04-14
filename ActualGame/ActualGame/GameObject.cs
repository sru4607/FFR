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
    class GameObject
    {
        #region Fields
        // Fields
        protected Rectangle rect;
        protected Texture2D texture;
        public bool noClip = false;
        #endregion

        #region Properties
        /// <summary>
        /// Returns whether the game object can collide with the player
        /// </summary>
        public bool NoClip
        {
            get { return noClip; }
            set { noClip = value; }
        }

        // TODO: Add description

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set { X = (int)value.X; Y = (int)value.Y; }
        }
        /// <summary>
        /// Get and set for the X value of the game object hitbox's top-left corner
        /// </summary>
        public int X
        {
            get { return rect.X; }
            set { rect = new Rectangle(value, Rect.Y, Rect.Width, Rect.Height); }
        }

        /// <summary>
        /// Get and set for the Y value of the game object hitbox's top-left corner
        /// </summary>
        public int Y
        {
            get { return rect.Y; }
            set { rect = new Rectangle(Rect.X, value, Rect.Width, Rect.Height); }
        }

        /// <summary>
        /// Get and set for the width of the game object hitbox
        /// </summary>
        public int Width
        {
            get { return rect.Width; }
            set { rect = new Rectangle(Rect.X, Rect.Y, value, Rect.Height); }
        }

        /// <summary>
        /// Get and set for the height of the game object hitbox
        /// </summary>
        public int Height
        {
            get { return rect.Height; }
            set { rect = new Rectangle(Rect.X, Rect.Y, Rect.Width, value); }
        }

        /// <summary>
        /// Get and set for the entirety of the hitbox as a Rectangle
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Generic GameObject without parameters
        /// </summary>
        public GameObject()
        {
            rect = new Rectangle();
            World current = new World("");
        }

        /// <summary>
        /// Creates a generic GameObject with the provided Rectangle dimensions
        /// </summary>
        /// <param name="x">X dimension of the draw box</param>
        /// <param name="y">Y dimension of the draw box</param>
        /// <param name="width">Width of the draw box</param>
        /// <param name="height">Height of the draw box</param>
        public GameObject(int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
        }
        #endregion

        #region LoadTexture
        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture">Content.Load&lt;Texture2D&gt;("INSERTLOCATION")</param>
        public void LoadTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        #endregion

        #region Methods
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        virtual public void Update(GameTime gm)
        {
            if (this is PhysicsObject temp)
                ((PhysicsObject)temp).Update(gm);

        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the object to the screen based off rect's parameters
        /// </summary>
        /// <param name="sb"></param>
        virtual public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
        #endregion
    }
}
