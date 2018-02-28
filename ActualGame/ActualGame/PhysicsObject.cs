using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualGame
{
    class PhysicsObject
    {
        int x;
        int y;
        int velX;
        int velY;

        void Move(int x, int y)
        {
            x = x + velX;
            y = y + velY;
        }
        void Gravity()
        {
            velY + 9.8*
        }


    }
}
