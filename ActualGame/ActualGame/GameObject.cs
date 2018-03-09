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
        Rectangle rect;
        double velX;
        double velY;
        Texture2D texture;


        private void Move()
        {
            X = (int)(X + velX);
            Y = (int)(Y + velY);
        }
        private void Gravity(double time)
        {
            velY += 9.8 * time;
        }
        virtual public void Update()
        {
            Move();
            Collision();


        }
        virtual public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
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
        private void Collision()
        {

        }


    }
}
