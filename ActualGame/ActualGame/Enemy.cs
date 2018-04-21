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
        protected Rectangle source;
        protected int animationFrames;
        protected const int framesBetweenAnimation = 10;
        private const int singleTextHeight = 64;
        private const int singleTextWidth = 32;
        protected double timeCounter;
        protected double secondsPerFrame;

        Rectangle temp;
        
        #endregion

        #region Properties
        public EnemyState State { get { return enemyState; } set { enemyState = value; animationFrames = 0; } }
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
            noClip = false;

            // Initialize animation parameters
            currentFrame = 0;
            secondsPerFrame = 1.0f / 30.0f;
        }
        #endregion

        #region Methods
        public new void TakeDamage(int damageAmount)
        {
            // TODO: Implement invincibility frames
            if (damageAmount >= hp)
            {
                hp = 0;
                Die();
            }
            else
            {
                hp -= damageAmount;
                mainAi.StunnedFrames = 10; // stuns the enemy for 10 frames
                State = EnemyState.Damaged;
                Movement = new Vector2(0, -5);
            }
        }

        /// <summary>
        /// Creates an exact copy of this enemy in a new quad tree
        /// </summary>
        /// <returns></returns>
        public Enemy Clone(Texture2D text, int hp, QuadTreeNode node = null)
        {
            Enemy clone = new Enemy((int)X, (int)Y, node, mainAi.PatrolType);
            clone.hp = hp;
            clone.texture = texture;

            return clone;
        }

        public new void Die()
        {
            // TODO: Implement during combat
            World.Current.AllObjects.Remove(this);
        }


        public new void Stun(int stunFrames)
        {
            // TODO: Implement during combat
        }

        public bool CharacterBlocked()
        {
            //node = node.GetContainingQuad(this);
            List<QuadTreeNode> parents = node.GetParents();
            temp = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            GameObject cha;
            if (mainAi.FacingRight) temp.Offset(20, 0);
            else temp.Offset(-20, 0);
            for (int i = 0; i < parents.Count; i++)
            {
                for (int j = 0; j < parents[i].Objects.Count; j++)
                {
                    cha = parents[i].Objects[j];
                    Rectangle chaRect = new Rectangle((int)cha.Position.X, (int)cha.Position.Y, (int)cha.Size.X, (int)cha.Size.Y);
                    if (cha != this && temp.Intersects(chaRect))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Update
        public override void Update(GameTime gm)
        {
            
            // TODO: Update so rect.Y is moved in the same call
            // NOTE: Do NOT call .MoveAI() twice, it will count as two frames of movement
            // NOTE: Also only moves in the X direction right now
            base.Update(gm);

            mainAi.MoveAI();

            switch (mainAi.PatrolType)
            {
                case PatrolType.Moving:
                    // Animation for moving enemy
                    source.Y = 1;
                    source.Width = singleTextWidth;
                    source.Height = singleTextHeight;
                    if (currentFrame % 15 == 0)
                    {
                        source.X += singleTextWidth;
                        if (currentFrame == 60)
                        {
                            currentFrame = 0;
                            source.X = 0;
                        }
                    }
                    currentFrame++;

                    break;
                case PatrolType.Standing:
                    source.Y = singleTextHeight + 1;
                    source.Width = singleTextWidth;
                    source.Height = singleTextHeight;
                    if (currentFrame % 15 == 0)
                    {
                        source.X += singleTextWidth;
                        if(currentFrame == 60)
                        {
                            currentFrame = 0;
                            source.X = 0;
                        }
                    }
                    currentFrame++;
                    break;
            }

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
                        sb.Draw(texture, position, source, Color.Red, 0, Vector2.Zero, new Vector2(Texture.Width/singleTextWidth, Height / Texture.Height), SpriteEffects.None, 0);
                    }
                    else
                    {
                        sb.Draw(texture, position, source, Color.Red, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.FlipHorizontally, 0);
                    }
                    break;
                case PatrolType.Standing:
                    if (mainAi.FacingRight)
                        sb.Draw(texture, Position, source, Color.White, 0, Vector2.Zero, new Vector2(texture.Width / singleTextWidth /4, texture.Height / singleTextHeight / 4), SpriteEffects.None, 0);
                    // Draws to the screen with a horizontal flip if the AI is facing left
                    else
                        sb.Draw(texture, Position, source, Color.White, 0, Vector2.Zero, new Vector2(texture.Width / singleTextWidth / 4, texture.Height / singleTextHeight / 4), SpriteEffects.None, 0);
                    break;
            }
            sb.Draw(Texture, temp, Color.Red);

        }
        #endregion
    }
}
