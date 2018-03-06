using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class Enemy : Character
    {
        AI mainAi;
        public Enemy()
        {
            mainAi = new AI();
        }
        public override void Update()
        {

        }
        public override void Draw(SpriteBatch sb)
        {

        }
    }
}
