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
        int theta;
        Rectangle maceSource;
        Rectangle bossSource;
        Rectangle chainSource;

        public Boss(Texture2D text, int x, int y, int width, int height)
        :base(x,y, new QuadTreeNode(x,y,width,height))
        {
            texture = text;
            HP = 100;
            maxHP = 100;
            bossSource = new Rectangle(x, y, width, height);
            
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

        public void UpdateBoss(GameTime gm)
        {
            theta += 6;
            //Check if Boss Is Hit

            //Update Chain
            chainSource = new Rectangle((int)Math.Cos(theta / 360 * 2 * Math.PI), (int)Math.Sin(theta / 360 * 2 * Math.PI), chainSource.Width, chainSource.Height);
            //Update Mace

            //Check if Player is Hit
            if(maceSource.Intersects(new Rectangle((int)Game1.player.position.X, (int)Game1.player.position.Y, (int)Game1.player.Size.X, (int)Game1.player.Size.Y)))
            {
                Game1.player.TakeDamage(1);
            }


            
        }

        public void DrawBoss(SpriteBatch sb)
        {
            //Draw Chain

            //Draw Boss


            //Draw Mace
        }

    }
}
