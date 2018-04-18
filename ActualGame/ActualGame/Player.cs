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
        int maxHealth;
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
        /// <summary>
        /// Movement with arrow keys
        /// </summary>
        public void KeyboardMovement()
        {
            KeyboardState kb = Keyboard.GetState();
            //Adjust movement based on keyboard controls
            if (kb.IsKeyDown(Keys.Left))
            { Movement = new Vector2(-5f, Movement.Y); }
            if (kb.IsKeyDown(Keys.Right))
            { Movement = new Vector2(5f, Movement.Y); }
            if (kb.IsKeyDown(Keys.Up) && OnGround())
            { Movement = new Vector2(Movement.X, -30f); }

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
        public override void Update(GameTime gm)
        {
            //Keyboard Movement   
            KeyboardMovement();
            //call physicsObject update
            base.Update(gm);
            switch (state)
            {
                case (PlayerState.Walk):
                {
                    if (kbState.IsKeyDown(Keys.Z))
                        {
                            state = PlayerState.MAttack;
                        }
                    break;
                }
                case (PlayerState.Jump):
                {


                    break;
                }
                case (PlayerState.Idle):
                {
                    if (kbState.IsKeyDown(Keys.Z))
                        {
                            state = PlayerState.MAttack;
                        }
                    break;
                }
                case (PlayerState.MAttack):
                {
                        this.MAttack();
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
                        sb.Draw(this.Texture, mBox, Color.Red); //meant to help check to make sure MAttack was going through. As of yet, hasn't seemed to work.
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
