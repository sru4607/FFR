using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace ActualGame
{
    class Boss : Character
    {
        int maxHP;

        public Boss(Texture2D text)
        :base(0,0, new QuadTreeNode(0,0,100,100))
        {
            texture = text;
            HP = 80;
            maxHP = 100;
        }


        public override void Die()
        {
            throw new NotImplementedException();
        }

        public void DrawHealthBar(SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(sb.GraphicsDevice.Viewport.Width/2 - 204, sb.GraphicsDevice.Viewport.Height - 54,408,40), Color.Black);
            sb.Draw(Texture, new Rectangle(sb.GraphicsDevice.Viewport.Width / 2 - 200, sb.GraphicsDevice.Viewport.Height - 50, (int)(((double)HP/maxHP)*400), 32), Color.Red);
        }

    }
}
