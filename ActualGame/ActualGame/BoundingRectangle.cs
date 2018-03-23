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
    }
}
