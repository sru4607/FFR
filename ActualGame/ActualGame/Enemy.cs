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

        protected int currentFrame;
        protected Texture2D walkTexture;
        protected int numWalkFrames;
        protected double timeCounter;
        protected double secondsPerFrame;
        #endregion

        #region Properties
        public EnemyState State { get { return enemyState; } set { enemyState = value; } }
        public int StateLock { get { return stateFrameLock; } set { stateFrameLock = value; } }
        public Texture2D WalkTexture
        {
            get { return walkTexture; }
            set
            {
                walkTexture = value;
                numWalkFrames = walkTexture.Width / Texture.Width;
                currentFrame = 0;
            }
        }
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

            // Initialize animation parameters
            currentFrame = 0;
            secondsPerFrame = 1.0f / 30.0f;
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

        /// <summary>
        /// Creates an exact copy of this enemy in a new quad tree
        /// </summary>
        /// <returns></returns>
        public Enemy Clone(QuadTreeNode node = null)
        {
            Enemy clone = new Enemy((int)X, (int)Y, node, mainAi.PatrolType);
            clone.hp = hp;
            clone.texture = texture;

            return clone;
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
            
            //mainAi.MoveAI();

            // TODO: Add EnemyState updater

            switch (mainAi.PatrolType)
            {
                case PatrolType.Moving:
                    // Animation for moving enemy
                    timeCounter += gm.ElapsedGameTime.TotalSeconds;

                    if (timeCounter >= secondsPerFrame)
                    {
                        currentFrame++;

                        if (currentFrame == numWalkFrames)
                            currentFrame = 0;

                        timeCounter -= secondsPerFrame;
                    }

                    break;
                case PatrolType.Standing:
                    break;
            }

            base.Update(gm);
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch sb)
        {
            switch (mainAi.PatrolType)
            {
                case PatrolType.Moving:
                    if (mainAi.FacingRight)
                    {
                        sb.Draw(walkTexture, position, new Rectangle(currentFrame * Texture.Width, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Texture.Width, Texture.Height), SpriteEffects.None, 0);
                    }
                    else
                    {
                        sb.Draw(walkTexture, position, new Rectangle(currentFrame * Texture.Width, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Texture.Width, Texture.Height), SpriteEffects.FlipHorizontally, 0);
                    }
                    break;
                case PatrolType.Standing:
                    if (mainAi.FacingRight)
                        sb.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Texture.Width, Texture.Height), SpriteEffects.None, 0);
                    // Draws to the screen with a horizontal flip if the AI is facing left
                    else
                        sb.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Texture.Width, Texture.Height), SpriteEffects.FlipHorizontally, 0);
                    break;
            }
        }
        #endregion
    }
}
