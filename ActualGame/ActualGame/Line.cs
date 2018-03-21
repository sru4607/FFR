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
    class Line
    {
        Point start;
        Point end;
        
        //creates a line with starting point s, and ending point e
        public Line(Point s, Point e)
        {
            start = s;
            end = e;
        }

        //returns the smallest distance between the line and a point;
        public double ClosestDist(Point other)
        {
            Line temp = new Line(start, other);
            Vector2 unit = new Vector2((float)Slope.X / (float)Length, (float)Slope.Y / (float)Length);
            return (unit.X * temp.Slope.X + unit.Y * temp.Slope.Y);
        }

        //Returns/sets the start point
        public Point Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        //returns/sets the end point
        public Point End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        //returns the slope in vector form
        public Vector2 Slope
        {
            get
            {
                return new Vector2(End.X - Start.X , End.Y - Start.Y);
            }
        }

        //returns the perpendicular slope in vector form
        public Vector2 PerpSlope
        {
            get
            {
                return new Vector2(-1 * (End.Y - Start.Y), End.X - Start.X);
            }
        }

        //returns the length of the line as a double
        public double Length
        {
            get
            {
                return Math.Sqrt((start.X - end.X) ^ 2 + (start.Y - end.Y) ^ 2);
            }
        }


    }
}
