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
        float theta;
        Circle maceSource;
        Rectangle bossSource;
        Rectangle chainSource;
        Texture2D barText;
        Texture2D bossText;
        Texture2D chainText;
        Texture2D maceText;

        public Boss(Texture2D bar, Texture2D boss, Texture2D chain,Texture2D mace, int x, int y, int width, int height)
        :base(x,y, new QuadTreeNode(x,y,width,height))
        {
            barText = bar;
            bossText = boss;
            chainText = chain;
            maceText = mace;
            HP = 1000;
            maxHP = 1000;
            bossSource = new Rectangle(x, y, bossText.Width, bossText.Height);

            maceSource = new Circle(bossSource.X + 256 + 512, bossSource.Y + 128, 128);

            chainSource = new Rectangle(bossSource.X + 128, bossSource.Y + 128, 128, 8);
            
        }


        public override void Die()
        {
            throw new NotImplementedException();
        }

        public void DrawHealthBar(SpriteBatch sb)
        {
            sb.Draw(barText, new Rectangle(sb.GraphicsDevice.Viewport.Width/2 - 204, sb.GraphicsDevice.Viewport.Height - 54,408,40), Color.Black);
            sb.Draw(barText, new Rectangle(sb.GraphicsDevice.Viewport.Width / 2 - 200, sb.GraphicsDevice.Viewport.Height - 50, (int)(((double)HP/maxHP)*400), 32), Color.Red);
        }

        public void UpdateBoss(GameTime gm)
        {
            theta += 3f;
            //Check if Boss Is Hit
            if(Game1.player.MBox.Intersects(bossSource) && Game1.player.State == PlayerState.MAttack)
            {
                HP -= 10;
            }
            //Update Chain
            //Update Mace
            bool temp = maceSource.Intersects(new Rectangle((int)Game1.player.position.X, (int)Game1.player.position.Y, (int)Game1.player.Size.X, (int)Game1.player.Size.Y));
            maceSource.X = (int)(bossSource.X + 256 + 512 * Math.Cos((double)theta / 360 * 2 * Math.PI));
            maceSource.Y = (int)(bossSource.Y + 128 + 512 * Math.Sin((double)theta / 360 * 2 * Math.PI));
            //Check if Player is Hit
            if (maceSource.Intersects(new Rectangle((int)Game1.player.position.X, (int)Game1.player.position.Y, (int)Game1.player.Size.X, (int)Game1.player.Size.Y)) && !temp)
            {
                Game1.player.TakeDamage(1);
            }
            if(HP <= 0)
            {
                Game1.CurrentState = MainGameState.Victory;
            }


            
        }

        public void DrawBoss(SpriteBatch sb)
        {
            //Draw Chain
            //sb.Draw(chainText, chainSource, new Rectangle(0, 0, 256, 8), Color.White, theta,new Vector2(-500, 0), SpriteEffects.None, 0f);
            //Draw Boss
            sb.Draw(bossText, bossSource,new Rectangle(0,0,256,256), Color.White,0,new Vector2(0,0),SpriteEffects.None, 0);

            //Draw Mace
            sb.Draw(maceText, maceSource.ToRectangle() ,new Rectangle(0,0,256,256), Color.White, 0f ,Vector2.Zero ,SpriteEffects.None, 0f);
        }

    }
}
