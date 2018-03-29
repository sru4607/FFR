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
    abstract class BoundingShapes
    {
        #region Fields
        protected Point location;
        #endregion

        #region AbstractMethods
        //protected abstract Point CorrectCollision();
        #endregion

        #region Method
        protected float DistancePoint(Point One, Point Two)
        {
            Vector2 VOne = One.ToVector2();
            Vector2 VTwo = Two.ToVector2();
            Vector2 VOneMinusTwo = VOne - VTwo;
            return VOneMinusTwo.Length();
        }

        //Checks Collisions between two objects one rectangle and one circle
        //private bool CircleRectangle(BoundingCircle circle, BoundingRectangle rect)
        //{
        //    //Credit for this code - https://yal.cc/rectangle-circle-intersection-test/
        //    float DeltaX = circle.Location.X - Math.Max(rect.Location.X, Math.Min(circle.Location.X, rect.Location.X + rect.GetRect.Width));
        //    float DeltaY = circle.Location.Y - Math.Max(rect.Location.Y, Math.Min(circle.Location.Y, rect.Location.Y + rect.GetRect.Height));
        //    return (DeltaX * DeltaX + DeltaY * DeltaY) < (circle.Radius * circle.Radius);
        //}

        public bool CheckCollision(BoundingShapes other)
        {
            //if (this is BoundingRectangle)
            //{
            //    if (other is BoundingCircle)
            //    {
            //        return CircleRectangle((BoundingCircle)other, (BoundingRectangle)this);
            //    }
            //    else if (other is BoundingRectangle)
            //    {
                    BoundingRectangle self = (BoundingRectangle)this;
                    return self.RectangleRectangle((BoundingRectangle)other);
            //    }
            //}
            //else if (this is BoundingCircle)
            //{
            //    if (other is BoundingRectangle)
            //    {
            //        return CircleRectangle((BoundingCircle)this, (BoundingRectangle)other);
            //    }
            //    else if (other is BoundingCircle)
            //    {
            //        BoundingCircle self = (BoundingCircle)this;
            //        return self.CircleCircle((BoundingCircle)other);
            //    }
            //}
            //return false;
        }
        #endregion

        #region Property
        public virtual Point Location
        {
            get { return location; }
            set { location = value; }
        }
        #endregion
    }
}
