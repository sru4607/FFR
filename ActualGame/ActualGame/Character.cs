using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class Character : GameObject
    {
        //Fields
        int hp;

        //Properties
        int HP
        {
            get { return hp; }
            set { hp = value; }
        }


        public Character()
            :base()
        {
            
        }


        public override void Update()
        {
            base.Update();
        }


        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
