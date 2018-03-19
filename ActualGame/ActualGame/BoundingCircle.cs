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
        Point center;
        double radius;
        

        public BoundingCircle(int x, int y, double radius)
        {
            center = new Point(x, y);
            this.radius = radius;
        }

        public bool CheckCollision(BoundingCircle other)
        {
            double totalDist = Radius + other.Radius;
            double currentDist = DistTo(other.Center);
            return (currentDist < totalDist);

        }
        public bool CheckCollision(Rectangle other)
        {
            if()
            return false;
        }
        public double DistTo(Point other)
        {

        }
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
