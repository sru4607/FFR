using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ActualGame
{
    class HitBoxes
    {
        Rectangle singleBox;
        GameObject parent;

        public HitBoxes(GameObject par, int x, int y, int width, int height)
        {
            parent = par;
            singleBox = new Rectangle(x, y, width, height);
        }


        public bool hasCollided(GameObject other)
        {
            return (singleBox.Intersects(other.HitBox.Rect));
        }
        public bool hasCollided(HitBoxes other)
        {
            return (singleBox.Intersects(other.Rect));
        }
        public void Move(float x, float y)
        {
            singleBox = new Rectangle((int)(Rect.X + x), (int)(Rect.Y + y), Rect.Width, Rect.Height);
        }

        
        public void Update()
        {
            if (parent.Rect != parent.Prev)
            {
                Move(parent.Velocity.X, parent.Velocity.Y);
            }
        }


        public Rectangle Rect
        {
            get
            {
                return singleBox;
            }
            set
            {
                singleBox = value;
            }
        }


    }
}
