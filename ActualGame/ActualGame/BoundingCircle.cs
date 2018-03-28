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
        #region Fields
            float radius;
        #endregion

        #region properties
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
        #endregion

        #region Constructor

        #endregion

        #region Methods

            //protected override Point CorrectCollision()
            //{
            //    throw new NotImplementedException();
            //}

            //Checks Collisions between two CircleObjects
            public bool CircleCircle(BoundingCircle other)
            {
                return DistancePoint(location, other.location) < radius + other.radius;
            }
        #endregion





    }
}
