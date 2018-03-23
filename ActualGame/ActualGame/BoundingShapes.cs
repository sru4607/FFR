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
        protected Point location;
        protected abstract Point CorrectCollision();

        protected float DistancePoint(Point One, Point Two)
        {
            Vector2 VOne = One.ToVector2();
            Vector2 VTwo = Two.ToVector2();
            Vector2 VOneMinusTwo = VOne - VTwo;
            return VOneMinusTwo.Length();
        }

        //Checks Collisions between two objects one rectangle and one circle
        private bool CircleRectangle(BoundingCircle circle , BoundingRectangle rect)
        {
            throw new NotImplementedException();
        }

    }
}
