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
        public override void Update(GameTime gameTime)
        {
            //base.Update();
            prevState = kbState;
            kbState = Keyboard.GetState();
            switch (state)
            {
                case (PlayerState.Walk):
                {
                    
                    if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Left))
                    {
                        velX = 3;
                        state = PlayerState.Walk;
                        if (kbState.IsKeyDown(Keys.Right))
                        {
                             
                            right = true;
                        }
                        if (kbState.IsKeyDown(Keys.Left))
                        {
                            right = false;
                        }
                        
                        
                    }
                    else
                    {
                        state = PlayerState.Idle;
                    }
                    Move(right);

                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        state = PlayerState.Jump;
                    } 
                    break;
                }
                case (PlayerState.Jump):
                {
                    velY = -5;
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
                    }
                    else
                    {
                        state = PlayerState.Idle;
                    }
                    Move(right);
                    break;
                }
                case (PlayerState.Idle):
                {
                    velX = 0;
                    //velY = 0;
                    if (kbState.IsKeyDown(Keys.Right))
                    {
                        right = true;
                        state = PlayerState.Walk;
                    }
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        right = false;
                        state = PlayerState.Walk;
                    }
                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        state = PlayerState.Jump;
                    }
                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        state = PlayerState.Jump;
                    }
                        Move(right);
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
