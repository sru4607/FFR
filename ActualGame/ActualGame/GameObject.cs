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
        /// Adjusts X and Y for the velocities
        /// </summary>
        private void Move()
        {
            X = (int)(X + velX);
            Y = (int)(Y + velY);
        }

        /// <summary>
        /// Applies Gravity
        /// </summary>
        /// <param name="time"></param>
        private void Gravity(double time)
        {
            velY += 9.8 * time;
        }

        /// <summary>
        /// Moves and checks for collisions mean to be overwritten
        /// </summary>
        virtual public void Update()
        {
            Move();
            Collision();

            
        }

        /// <summary>
        /// Draws the object with the texture, at the set position, with the color white
        /// </summary>
        /// <param name="sb"></param>
        virtual public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
        
        /// <summary>
        /// returns or sets the X coordinate of the object
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
        /// returns/sets the Y cordinate of the object
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
        /// gets or sets the width
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
        /// Gets or sets height
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
        /// Checks for collisions
        /// </summary>
        private bool Collision()
        {
            return false;
        }


    }
}
