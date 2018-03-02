using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualGame
{
    class PhysicsObject
    {
        double x;
        double y;
        double velX;
        double velY;

        void Move(double x, double y)
        {
            x = x + velX;
            y = y + velY;
        }
        void Gravity(double time)
        {
            velY += 9.8 * time;
        }


    }
}
