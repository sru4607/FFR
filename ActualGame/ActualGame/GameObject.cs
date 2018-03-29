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
        protected Rectangle prev;
        protected int step;
        static float grav = 9.8f;
        protected bool physicsObject = false;
        public BoundingShapes hitbox;
        public bool noClip = false;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool NoClip
        {
            get { return noClip; }
            set { noClip = value; }
        }
        public bool Finished
        {
            get
            {
                return step >= 15;
            }
        }

        public int X
        {
            get { return rect.X; }
            set { rect = new Rectangle(value, Rect.Y, Rect.Width, Rect.Height); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get { return rect.Y; }
            set { rect = new Rectangle(Rect.X, value, Rect.Width, Rect.Height); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get { return rect.Width; }
            set { rect = new Rectangle(Rect.X, Rect.Y, value, Rect.Height); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get { return rect.Height; }
            set { rect = new Rectangle(Rect.X, Rect.Y, Rect.Width, value); }
        }

        /// <summary>
        /// Interacts with all parameters of rect
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        public BoundingShapes HitBox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }

        public bool Physics
        {
            get { return physicsObject; }
            set { physicsObject = value; }
        }
        #endregion

        #region Constructor
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
        public void Move()
        {
            prev = rect;
            step = 0;
            X = (int)(X + velX);
            Y = (int)(Y + velY);
            if(hitbox != null)
            {
                hitbox.Location = this.Rect.Location;
            }
        }

        public void Move(bool right)
        {
            prev = rect;
            step = 0;
            if (right)
            {
                X = (int)(X + velX);
            }
            else
            {
                X = (int)(X - velX);
            }
            Y = (int)(Y + velY);
            /* check for collisions with the wall, if colliding, revert to previous state and set speed to 0 */
        }
        
        public void Step()
        {
            if(step < 15)
            {
                X = (int)(X + velX / 16);
                Y = (int)(Y + velY / 16);
                step++;
            }

        }
        
        public void StepBack()
        {
            X = (int)(X - velX / 16);
            Y = (int)(Y - velY / 16);
        }
        public void Revert()
        {
            rect = prev;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Gravity()
        {
            velY += grav/60;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Collision(List<GameObject> temp)
        {
            for(int i = 0; i< temp.Count; i++)
            {
                if(this != temp[i] && !temp[i].noClip)
                {
                    if (this.rect.Intersects(temp[i].Rect))
                    {
                        //this.Revert();
                        velY = 0;
                    }
                        
                    
                }
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        virtual public void Update()
        {
            if (this is Player temp)
                temp.Update();

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
    }
}
