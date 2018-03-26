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
    class BoundingCircle : BoundingShapes
    {
        float radius;
        protected override Point CorrectCollision()
        {
            throw new NotImplementedException();
        }

        //Checks Collisions between two CircleObjects
        public bool CircleCircle (BoundingCircle other)
        {
            return DistancePoint(location, other.location) < radius+other.radius;
        }

        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }
    }
}
