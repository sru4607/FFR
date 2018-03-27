using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ActualGame
{
    class BoundingRectangle : BoundingShapes
    {
        Rectangle mainRect;

        public BoundingRectangle(Point center, float width, float height)
        {
            mainRect = new Rectangle((center.X - (int)width / 2), (center.Y - (int)height / 2), (int)width, (int)height);
        }

        protected override Point CorrectCollision()
        {
            throw new NotImplementedException();
        }

        public bool RectangleRectangle(BoundingRectangle other)
        {
            Rectangle otherRect = other.GetRect;
            if (mainRect.Intersects(otherRect))
            {
                return true;
            }
            return false;
        }


        public Rectangle GetRect
        {
            get
            {
                return mainRect;
            }
        }

        public void MoveRect(Point center)
        {
            mainRect.Location = new Point(center.X - mainRect.Width / 2, center.Y - mainRect.Height / 2);
        }
    }
}
