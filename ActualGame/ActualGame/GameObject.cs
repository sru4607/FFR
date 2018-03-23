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
        protected double velX;
        protected double velY;
        protected Texture2D texture;
        #endregion

        #region Initialization
        public GameObject()
        {
            // TODO Update these fields (possibly have GameObject() take parameters)
            rect = new Rectangle();
            velX = 0.0;
            velY = 0.0;
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
        /// <summary>
        /// 
        /// </summary>
        private void Move()
        {
            X = (int)(X + velX);
            Y = (int)(Y + velY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        private void Gravity(double time)
        {
            velY += 9.8 * time;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Collision()
        {

        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        virtual public void Update()
        {
            Move();
            Collision();

            
        }
        #endregion

        #region Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        virtual public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int X
        {
            get { return rect.X; }
            set { rect = new Rectangle(value, Y, Width, Height); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get {  return rect.X; }
            set { rect = new Rectangle(X, value, Width, Height); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get { return rect.X; }
            set { rect = new Rectangle(X, Y, value, Height); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get { return rect.Height; }
            set { rect = new Rectangle(X, Y, Width, value); }
        }

        /// <summary>
        /// Interacts with all parameters of rect
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        #endregion

        // (Kevin) TODO: Update Draw() to use drawBox; move drawBox from Character.cs to GameObject.cs
    }
}
