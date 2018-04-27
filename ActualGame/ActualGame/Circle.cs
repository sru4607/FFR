using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ActualGame
{
    class Circle
    {
        public int Radius { get; set; }
        public int X;
        public int Y;

        public Circle(int x, int y, int radius)
        {
            Radius = radius;
            X = x;
            Y = y;
        }

        public bool Intersects(Rectangle rect)
        {
            // the first thing we want to know is if any of the corners intersect
            Point[] points = new Point[4];
            points[0] = new Point(rect.X, rect.Y);
            points[1] = new Point(rect.X+rect.Width, rect.Y);
            points[2] = new Point(rect.X, rect.Y+rect.Height);
            points[3] = new Point(rect.X+rect.Width, rect.Y+rect.Height);

            foreach (Point p in points)
            {
                if (ContainsPoint(p))
                    return true;
            }

            // next we want to know if the left, top, right or bottom edges overlap
            if (X - Radius > rect.Right || X + Radius < rect.Left)
                return false;

            if (Y - Radius > rect.Bottom || Y + Radius < rect.Top)
                return false;

            return true;
        }

        public bool ContainsPoint(Point p)
        {
            Vector2 v2 = new Vector2(p.X - X, p.Y - Y);
            if(v2.Length() < Radius)
            {
                return true;
            }
            return false;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle();
        }

    }
}

