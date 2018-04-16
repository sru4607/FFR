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
        EnemyState enemyState;
        int stateFrameLock;
        #endregion

        #region Properties
        public EnemyState State { get { return enemyState; } set { enemyState = value; } }
        public int StateLock { get { return stateFrameLock; } set { stateFrameLock = value; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a generic Enemy
        /// </summary>
        /// <param name="x">X location of the </param>
        /// <param name="y"></param>
        /// <param name="node">Reference to the quad tree used in-game</param>
        /// <param name="patrolType"></param>
        public Enemy(int x, int y, QuadTreeNode node, PatrolType patrolType)
            // Defaults to a width of 64 and a height of 128
            : base(x, y, 64, 128, node)
        {
            // Initialize the AI pattern
            mainAi = new AI(this, patrolType);

            // Initialize hitbox parameters
            Position = new Vector2(X,Y);
            Size = new Vector2(64, 128);
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
        public override void Update(GameTime gm)
        {
            // TODO: Update so rect.Y is moved in the same call
            // NOTE: Do NOT call .MoveAI() twice, it will count as two frames of movement
            // NOTE: Also only moves in the X direction right now
            
            mainAi.MoveAI();

            base.Update(gm);
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch sb)
        {
            if(mainAi.FacingRight)
                sb.Draw(texture, Position, new Rectangle(0,0,texture.Width,texture.Height), Color.White,0, Vector2.Zero, new Vector2(Width / texture.Width, Height / texture.Height), SpriteEffects.None, 0);
            // Draws to the screen with a horizontal flip if the AI is facing left
            else
                sb.Draw(texture, Position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width/ texture.Width, Height / texture.Height), SpriteEffects.FlipHorizontally, 0);
        }
        #endregion
    }
}
