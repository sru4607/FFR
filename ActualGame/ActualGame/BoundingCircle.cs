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
    class BoundingCircle
    {
        //fields
        Point center;
        double radius;
        
        /// <summary>
        /// Creates a bounding circle
        /// </summary>
        /// <param name="x">the center's X position</param>
        /// <param name="y">The center's Y position</param>
        /// <param name="radius">the circle's radius</param>
        public BoundingCircle(int x, int y, double radius)
        {
            center = new Point(x, y);
            this.radius = radius;
        }
        /// <summary>
        /// Checks collisions between two circles
        /// </summary>
        /// <param name="other">the other object</param>
        /// <returns>returns if colliding</returns>
        public bool CheckCollision(BoundingCircle other)
        {
            double totalDist = Radius + other.Radius;
            double currentDist = DistTo(other.Center);
            return (currentDist < totalDist);

        }

        //Will work on this method later

        bool CheckCollision(Rectangle other)
        {
            if (other.Contains(Center))
            {
                return true;
            }
            else
            {

            }
                return false;
        }

        public double DistTo(Point other)
        {
            return (Math.Sqrt((Center.X - other.X) ^ 2 + (Center.Y - other.Y) ^ 2));
        }

        //Property for the Radius
        public double Radius
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
        //Property for the Center
        public Point Center
        {
            get
            {
                return Center;
            }
            set
            {
                Center = value;
            }
        }
        
    }
}
