using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ActualGame
{
    //Enumeration for the FSM for the Player actions
    public enum PlayerState { Walk, Jump, Idle, MAttack, Crouch, Interact, Dead}

    class Player : Character, ICombat
    {
        #region Fields
        PlayerState state;
        KeyboardState kbState;
        KeyboardState prevState;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public Player(bool right = true)
            : base(right)
        {
            hitbox = new BoundingRectangle(this.Rect.Center, this.Rect.Width * 0.95f, this.Rect.Height * 0.95f);

            state = PlayerState.Idle;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reduces the health of the Player when they take damage, and calls the Die method if necessary
        /// </summary>
        /// <param name="damageAmount">The amount of damage taken</param>
        public new void TakeDamage(int damageAmount)
        {
            if (damageAmount > hp)
            {
                hp = 0;
                Die();
            }
            else
                hp -= damageAmount;
        }

        //TODO: Method to call that should update the game to signal the Player has died
        public new void Die()
        {

        }

        //TODO: Method to stun the player for the amount of time chosen
        public new void Stun(int stunFrames)
        {

        }
        #endregion

        #region Update
        public override void Update()
        {
            //base.Update();
            prevState = kbState;
            kbState = Keyboard.GetState();
            switch (state)
            {
                case (PlayerState.Walk):
                {
                    velX = 10;
                    velY = 0;
                    if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Left))
                    {
                        state = PlayerState.Walk;
                        if (kbState.IsKeyDown(Keys.Right))
                        {
                            right = true;
                        }
                        if (kbState.IsKeyDown(Keys.Left))
                        {
                            right = false;
                        }
                        Gravity();
                        Move(right);
                    }
                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        state = PlayerState.Jump;
                    } 
                    break;
                }
                case (PlayerState.Jump):
                {
                    velY = -10;
                    Gravity();
                    if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Left))
                    {
                        state = PlayerState.Walk;
                        if (kbState.IsKeyDown(Keys.Right))
                        {
                            right = true;
                        }
                        if (kbState.IsKeyDown(Keys.Left))
                        {
                            right = false;
                        }
                        Move(right);
                    }
                    break;
                }
                case (PlayerState.Idle):
                {
                    velX = 0;
                    velY = 0;
                    Gravity();
                    if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Left))
                    {
                        state = PlayerState.Walk;
                        if (kbState.IsKeyDown(Keys.Right))
                        {
                            right = true;
                        }
                        if (kbState.IsKeyDown(Keys.Left))
                        {
                            right = false;
                        }
                        if (kbState.IsKeyDown(Keys.Up))
                        {
                            state = PlayerState.Jump;
                        }
                        Gravity();
                        Move(right);
                    }
                    break;
                }
                case (PlayerState.MAttack):
                {
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
                    break;
                }
                case (PlayerState.Jump):
                {
                    break;
                }
                case (PlayerState.Idle):
                {
                    break;
                }
                case (PlayerState.MAttack):
                {
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
            base.Draw(sb);
        }
        #endregion








        
    }
}
