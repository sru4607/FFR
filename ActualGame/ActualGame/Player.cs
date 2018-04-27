using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ActualGame
{
    //Enumeration for the FSM for the Player actions
    public enum PlayerState { Walk, Jump, Idle, MAttack, Crouch, Interact, Dead}

    public class Player : Character, ICombat

    {

        //player references the static class Controls for each control
        #region Fields
        PlayerState state;
        KeyboardState kbState;
        KeyboardState prevState;
        int maxHealth;

        protected int currentFrame;
        protected Texture2D walkTexture;
        protected int numWalkFrames;
        protected double timeCounter;
        protected double secondsPerFrame;
        #endregion

        #region Properties
        //return if dead
        public bool IsDead
        {
            get { return state == PlayerState.Dead;}
        }
        //only get MaxHealth
        public int MaxHealth
        {
            get { return maxHealth; }
        }

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
        /// Creates a generic Player class at the X, Y coordinates provided
        /// </summary>
        /// <param name="x">Starting X location of the player</param>
        /// <param name="y">Starting Y location of the player</param>
        /// <param name="node">Reference to the QuadTree used in Game1(?)</param>
        /// <param name="right">Whether the player starts facing right</param>
        public Player(int x, int y, QuadTreeNode node, bool right = true)
            // Defaults to a hitbox width of 64 and hitbox height of 128
            : base(x, y, 64, 128, node, right)
        {
            state = PlayerState.Idle;
            maxHealth = 3;
            hp = 3;
            mDamage = 1;

            // Initialize animation parameters
            currentFrame = 0;
            secondsPerFrame = 1.0f / 30.0f;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reduces the health of the Player when they take damage, and calls the Die method if necessary
        /// </summary>
        /// <param name="damageAmount">The amount of damage taken</param>
        public new void TakeDamage(int damageAmount)
        {
            hp -= damageAmount;
            if (hp <= 0)
                Die();
        }
        /// <summary>
        /// Movement with arrow keys
        /// </summary>
        public void KeyboardMovement()
        {
            kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Controls.Left) && kbState.IsKeyUp(Controls.Right) && (state == PlayerState.Walk || state == PlayerState.Jump))
            {
                Movement = new Vector2(-5f, Movement.Y);
                right = false;
            }
            if (kbState.IsKeyDown(Controls.Right) && kbState.IsKeyUp(Controls.Left) && (state == PlayerState.Walk || state == PlayerState.Jump))
            {
                Movement = new Vector2(5f, Movement.Y);
                right = true;
            }
            if (kbState.IsKeyDown(Controls.Jump) && OnGround() && state == PlayerState.Jump)
            { Movement = new Vector2(Movement.X, -30f); }

        }

        //TODO: Method to call that should update the game to signal the Player has died
        public override void Die()
        {
            state = PlayerState.Dead;
        }

        //TODO: Method to stun the player for the amount of time chosen
        public new void Stun(int stunFrames)
        {

        }
        #endregion

        #region Update
        public override void Update(GameTime gm)
        {
            
            KeyboardMovement();
            base.Update(gm);
            switch (state)
            {
                case (PlayerState.Walk):
                {
                    

                    // Animation for moving player
                    timeCounter += gm.ElapsedGameTime.TotalSeconds;

                    if (timeCounter >= secondsPerFrame)
                    {
                        currentFrame++;

                        if (currentFrame == numWalkFrames)
                            currentFrame = 0;

                        timeCounter -= secondsPerFrame;
                    }

                    if (kbState.IsKeyDown(Controls.Jump))
                        {
                            state = PlayerState.Jump;
                        }
                        if (kbState.IsKeyDown(Controls.Right))
                            right = true;
                        if (kbState.IsKeyDown(Controls.Left))
                            right = false;
                    if (kbState.IsKeyUp(Controls.Jump) && kbState.IsKeyUp(Controls.Left) && kbState.IsKeyUp(Controls.Right))
                        {
                            state = PlayerState.Idle;
                        }
                        if (kbState.IsKeyDown(Keys.Z))
                        {
                            state = PlayerState.MAttack;
                        }

                        break;
                }
                case (PlayerState.Jump):
                {
                        if (OnGround())
                        {
                            state = PlayerState.Idle;
                        }
                        if (kbState.IsKeyDown(Controls.Attack))
                        {
                            state = PlayerState.MAttack;
                        }
                    break;
                }
                case (PlayerState.Idle):
                {
                        if (kbState.IsKeyDown(Controls.Left))
                        {
                            state = PlayerState.Walk;
                            right = false;
                        }
                        if (kbState.IsKeyDown(Controls.Right))
                        {
                            state = PlayerState.Walk;
                            right = true;
                        }
                        if (kbState.IsKeyDown(Controls.Jump))
                        {
                            state = PlayerState.Jump;
                        }
                    if (kbState.IsKeyDown(Controls.Attack))
                        {
                            state = PlayerState.MAttack;
                        }
                    break;
                }
                case (PlayerState.MAttack):
                {
                        this.MAttack();

                        // setup for animations
                        timeCounter += gm.ElapsedGameTime.TotalSeconds;
                        if (timeCounter >= Game1.secondsPerFrame)
                        {
                            currentFrame++;
                            if (currentFrame >= 4) currentFrame = 1;

                            timeCounter -= Game1.secondsPerFrame;
                        }
                        for(int i = 0; i<World.Current.AllObjects.Count; i++)
                        {
                            if(World.Current.AllObjects[i] is Enemy e)
                            {
                                if(AttackIntersects(e))
                                {
                                    e.TakeDamage(10);
                                }
                            }
                        }

                        if (currentFrame == 3) // after finishing attack animation, go back to idle
                        {
                            state = PlayerState.Idle;
                        }
                    break;
                }
                case (PlayerState.Crouch):
                {
                    break;
                }
                case (PlayerState.Interact):
                {
                    break;
                }
                case (PlayerState.Dead):
                {
                    break;
                }
            }
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch sb)
        {
            switch (state)
            {
                case (PlayerState.Walk):
                {
                    if (right)
                    {
                       sb.Draw(walkTexture, position, new Rectangle(currentFrame * Texture.Width, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.None, 0);
                    }
                    else
                    {
                        sb.Draw(walkTexture, position, new Rectangle(currentFrame * Texture.Width, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.FlipHorizontally, 0);

                    }
                        break;
                }
                case (PlayerState.Idle):
                case PlayerState.Jump:
                {
                        if (right)
                        {
                            sb.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.None, 0);
                        }
                        else
                        {
                            sb.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.FlipHorizontally, 0);
                        }
                            break;
                }
                case (PlayerState.MAttack):
                {
                        //sb.Draw(Texture, MBox, Color.Red); // Just remove this line to make mBox invisible
                        if (right)
                        {
                            sb.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.None, 0);
                        }
                        else
                        {
                            sb.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.FlipHorizontally, 0);
                        }
                        break;
                }
                case (PlayerState.Crouch):
                {
                    break;
                }
                case (PlayerState.Interact):
                {
                    break;
                }
                case (PlayerState.Dead):
                {
                    break;
                }
            }
        }
        #endregion








        
    }
}
