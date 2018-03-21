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
        Vector2 slope;

        public Line(Point s, Point e)
        {
            start = s;
            end = e;
        }

        
    }
}
