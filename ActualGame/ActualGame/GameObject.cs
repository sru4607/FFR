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
        //fields
        Rectangle rect;
        double velX;
        double velY;
        Texture2D texture;

        //To Do:
        //Make an initialization method - Rectangle, Texture, Velocities



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
        virtual public void Update()
        {
            Move();
            Collision();

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        virtual public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int X
        {
            get
            {
                return rect.X;
            }
            set
            {
                rect = new Rectangle(value, Y, Width, Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get
            {
                return rect.X;
            }
            set
            {
                rect = new Rectangle(X, value, Width, Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get
            {
                return rect.X;
            }
            set
            {
                rect = new Rectangle(X, Y, value, Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get
            {
                return rect.Height;
            }
            set
            {
                rect = new Rectangle(X, Y, Width, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Collision()
        {

        }


    }
}
