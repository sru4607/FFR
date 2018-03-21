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

        //Checks for Collisions between a circular object and a rectangular object

        bool CheckCollision(Rectangle other)
        {
            //if rectangle contains the circle's center
            if (other.Contains(Center))
            {
                return true;
            }
            //if any of the rectangle's corners are in the circle
            else if(DistTo(other.Location) <= Radius || DistTo(other.X + other.Width, other.Y) <= Radius || DistTo(other.X + other.Width, other.Y+other.Height) <= Radius || DistTo(other.X, other.Y+ other.Height) <= Radius)
            {
                return true;
            }
            //if any side has a point on or in the circle
            else
            {
                Line top = new Line(other.Location, new Point(other.X + other.Width, other.Y));
                if(top.ClosestDist(center) <= radius)
                {
                    return true;
                }
                Line left = new Line(other.Location, new Point(other.X, other.Y + other.Height));
                if (left.ClosestDist(center) <= radius)
                {
                    return true;
                }
                Line bottom = new Line(new Point(other.X,other.Y + other.Height), new Point(other.X + other.Width, other.Y + other.Height));
                if (bottom.ClosestDist(center) <= radius)
                {
                    return true;
                }
                Line right = new Line(new Point(other.X + other.Width, other.Y), new Point(other.X + other.Width, other.Y + other.Height));
                if (right.ClosestDist(center) <= radius)
                {
                    return true;
                }
            }
                return false;
        }

        //Distance from the center to a point 
        public double DistTo(Point other)
        {
            return (Math.Sqrt((Center.X - other.X) ^ 2 + (Center.Y - other.Y) ^ 2));
        }

        //Distance from the center to a point with the cordinates X,Y
        public double DistTo(int x, int y)
        {
            Point temp = new Point(x, y);
            return DistTo(temp);
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
