using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

enum EnemyState { Docile, Search, Aggro, Damaged, Attack }
namespace ActualGame
{
    class Enemy : Character, ICombat
    {
        #region Fields
        AI mainAi;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        /// <summary>
        /// Creates a generic Enemy
        /// </summary>
        public Enemy()
            : base()
        {
            // Initialize the AI pattern
            mainAi = new AI(this, PatrolType.Moving);

            // Temporary values to render to screen
            rect.X = 400;
            rect.Y = 300;
            rect.Width = 128;
            rect.Height = 128;
            noClip = true;
        }
        #endregion

        #region Methods
        public new void TakeDamage(int damageAmount)
        {
            if (damageAmount >= hp)
            {
                hp = 0;
                Die();
            }
            else
                hp -= damageAmount;
        }


        public new void Die()
        {
            // TODO: Implement during combat
        }


        public new void Stun(int stunFrames)
        {
            // TODO: Implement during combat
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            // TODO: Update so rect.Y is moved in the same call
            // NOTE: Do NOT call .MoveAI() twice, it will count as two frames of movement
            // NOTE: Also only moves in the X direction right now
            rect.X += (int)mainAi.MoveAI();

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch sb)
        {
            if(mainAi.FacingRight)
                sb.Draw(texture, rect, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            // Draws to the screen with a horizontal flip if the AI is facing left
            else
                sb.Draw(texture, rect, null, Color.White, 0, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0);
        }
        #endregion
    }
}
