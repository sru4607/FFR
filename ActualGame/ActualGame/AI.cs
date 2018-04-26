using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PatrolType { Standing, Moving }
enum PatrolState { WalkLeft, PauseLeft, WalkRight, PauseRight }
namespace ActualGame
{
    /// <summary>
    /// Manages the movement of an Enemy class
    /// </summary>
    class AI
    {
        #region Fields
        PatrolType patrolType;
        PatrolState patrolState;
        Enemy enemy; // Reference variable to what the AI is controlling
        int frameCounter; // Counter for how many frames the AI has paused movement for
        int pauseLeft; // Number of frames to pause when facing left during the the docile movement pattern
        int pauseRight; // Number of frames to pause when facing right during the the docile movement pattern
        double walkSpeed; // How far the enemy moves in a single frame
        bool facingRight; // 0 is facing left, 1 is facing right
        int stunnedFrames; // Finite state variables
        #endregion

        #region Properties
        /// <summary>
        /// Returns 0 for facing left, or 1 for facing right
        /// </summary>
        public bool FacingRight
        {
            get { return facingRight; }
            set { facingRight = value; }
        }

        /// <summary>
        /// Gets the AI's patrolType
        /// </summary>
        public PatrolType PatrolType
        {
            get { return patrolType; }
        }

        public int StunnedFrames
        {
            get { return stunnedFrames; }
            set { stunnedFrames = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a generic patrol pattern
        /// </summary>
        /// <param name="enemy">Reference to the enemy that the AI is controlling</param>
        /// <param name="patrolType">Pattern of movement</param>
        public AI(Enemy enemy, PatrolType patrolType)
        {
            this.enemy = enemy;
            this.patrolType = patrolType;
            patrolState = PatrolState.PauseRight;
            facingRight = true;
            walkSpeed = 3.0;
            frameCounter = 0;

            // Pauses for 2 seconds on each side by default
            pauseLeft = 120;
            pauseRight = 120;

            // Initializes variables used for locking a finite state
            stunnedFrames = 0;
        }

        /// <summary>
        /// Initializes a more specific patrol pattern
        /// </summary>
        /// <param name="enemy">Reference to the enemy that the AI is controlling</param>
        /// <param name="patrolType">Pattern of movement</param>
        /// <param name="walkSpeed">The distance traveled in one frame of movement</param>
        /// <param name="pauseLeft">The number of frames for the enemy to pause after stopping while facing left</param>
        /// <param name="pauseRight">The number of frames for the enemy to pause after stopping while facing right</param>
        public AI(Enemy enemy, PatrolType patrolType, double walkSpeed, int pauseLeft = 120, int pauseRight = 120)
        {
            this.enemy = enemy;
            this.patrolType = patrolType;
            this.walkSpeed = walkSpeed;
            this.pauseLeft = pauseLeft;
            this.pauseRight = pauseRight;

            patrolState = PatrolState.PauseRight;
            frameCounter = 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the enemy's movement AI by 1 frame and returns the change in X position 
        /// Called through a class property
        /// </summary>
        public void MoveAI()
        {
            enemy.State = UpdateAggro();
            // Update the current finite state
            switch (enemy.State)
            {

                case EnemyState.Docile:
                    // Check to see whether something causes the enemy to leave a Docile state
                    // Make normal Docile movement patterns if the enemy's still docile
                    switch (patrolState)
                    {
                        case PatrolState.WalkLeft:
                            // If the enemy is unable to walk to the left, update to PauseLeft
                            if(!AbleToMove())
                            {
                                // If the enemy never pauses facing to the left, swap immediately to walking right
                                if (pauseLeft > 0)
                                {
                                    frameCounter = 0;
                                    patrolState = PatrolState.PauseLeft;
                                }
                                else
                                {
                                    patrolState = PatrolState.WalkRight;
                                    facingRight = true;
                                }
                            }
                            break;

                        case PatrolState.PauseLeft:
                            // If the enemy has stood still for pauseLeft frames, walk right
                            if(++frameCounter >= pauseLeft)
                            {
                                patrolState = PatrolState.WalkRight;
                                facingRight = true;
                            }
                            break;

                        case PatrolState.WalkRight:
                            if (!AbleToMove())
                            {
                                // If the enemy never pauses facing to the right, swap immediately to walking left
                                if (pauseRight > 0)
                                {
                                    frameCounter = 0;
                                    patrolState = PatrolState.PauseRight;
                                }
                                else
                                {
                                    patrolState = PatrolState.WalkLeft;
                                    facingRight = false;
                                }
                            }
                            break;

                        case PatrolState.PauseRight:
                            // If the enemy has stood still for pauseRight frames, walk left
                            if (++frameCounter >= pauseRight)
                            {
                                patrolState = PatrolState.WalkLeft;
                                facingRight = false;
                            }
                            break;

                        default:
                            throw new NotImplementedException("Unknown PatrolState case in AI.MoveAI()");
                    }
                    break;

                // If the enemy has been stunned for the number of frames, unstun them and return to docile
                // TODO: Update this to involve swapping to aggro
                case EnemyState.Damaged:
                    if (stunnedFrames == 0)
                        enemy.State = EnemyState.Docile;
                    else
                        stunnedFrames--;
                    break;

                default:
                    break;
            }

            // Move based off the current state
            switch (enemy.State)
            {
                // If the enemy is docile, follow the patrol route
                case EnemyState.Docile:
                    switch(patrolState)
                    {
                        case PatrolState.PauseLeft:
                        case PatrolState.PauseRight:
                            {
                                enemy.Movement = new Vector2(0f, enemy.Movement.Y);
                                break;
                            }
                        case PatrolState.WalkLeft:
                            enemy.Movement = new Vector2((-(float)walkSpeed), enemy.Movement.Y);
                            break;
                        case PatrolState.WalkRight:
                            enemy.Movement = new Vector2(((float)walkSpeed), enemy.Movement.Y);
                            break;
                    }
                    break;
                case EnemyState.Damaged:
                    enemy.Movement = new Vector2(0f, enemy.Movement.Y);
                    break;
                case EnemyState.Aggro:
                    if(Math.Abs(Game1.player.X - enemy.X) < 96)
                    {
                        enemy.State = EnemyState.Attack;
                        frameCounter = 0;
                    }
                    else if(Game1.player.X > enemy.X)
                    {
                        FacingRight = true;
                        enemy.Right = true;
                        patrolState = PatrolState.WalkRight;
                        if (AbleToMove())
                            enemy.Movement = new Vector2(((float)walkSpeed), enemy.Movement.Y);
                    }
                    else
                    { 
                        FacingRight = false;
                        enemy.Right = false;
                        patrolState = PatrolState.WalkLeft;
                        if (AbleToMove())
                            enemy.Movement = new Vector2((-(float)walkSpeed), enemy.Movement.Y);
                    }   
                    break;
                case EnemyState.Attack:
                    frameCounter++;
                    if(frameCounter == 60)
                    {
                        enemy.State = EnemyState.Aggro;
                    }
                    if (Game1.player.X > enemy.X)
                    {
                        FacingRight = true;
                        patrolState = PatrolState.WalkRight;
                    }
                    else
                    {
                        FacingRight = false;
                        patrolState = PatrolState.WalkLeft;
                    }
                    if (frameCounter == 45)
                    {
                        if(facingRight)
                        {
                            Rectangle temp = new Rectangle((int)enemy.X + 64, (int)enemy.Y, 32, 64);
                            if(temp.Intersects(new Rectangle((int)Game1.player.position.X, (int)Game1.player.position.Y, (int)Game1.player.Size.X, (int)Game1.player.Size.Y)))
                            {
                                Game1.player.HP--;
                            }
                        }
                        else
                        {
                            Rectangle temp = new Rectangle((int)enemy.X - 64, (int)enemy.Y, 32, 64);
                            if (temp.Intersects(new Rectangle((int)Game1.player.position.X, (int)Game1.player.position.Y, (int)Game1.player.Size.X, (int)Game1.player.Size.Y)))
                            {
                                Game1.player.HP--;
                            }
                        }
                    }
                    break;
                case EnemyState.Search:

                    break;
                // TODO: Add Search, Aggro, and Attack movements
                default:
                    throw new NotImplementedException($"AI movement not implemented for {Enum.GetName(typeof(EnemyState), enemy.State)} state in AI.MoveAI()");
            }
        }


        /// <summary>
        /// Helper method - determines what state of aggro the enemy will be in
        /// </summary>
        /// <returns>The current finite state of aggro</returns>
        private EnemyState UpdateAggro()
        {
            // TODO: Update this code to scan for the player
            if(enemy.State == EnemyState.Attack)
            {
                return EnemyState.Attack;
            }
            if(Math.Abs(Game1.player.X - enemy.X) < 1000 && Math.Abs(Game1.player.Y - enemy.Y) < 20)
            {
                return EnemyState.Aggro;
            }
            return EnemyState.Docile;
        }

        /// <summary>
        /// Sets the AI's finite state to the provided parameter
        /// </summary>
        /// <param name="state">Updated Enemy finite state</param>
        public void UpdateAggro(EnemyState state)
        {
            enemy.State = state;
        }

        /// <summary>
        /// Helper method with determining whether there is an area to walk forward to
        /// </summary>
        /// <returns>True if the enemy can walk forward, else false</returns>
        private bool AbleToMove()
        {
            return !(enemy.AtEdge(FacingRight) || enemy.AtWall(FacingRight) || CharacterBlocked());
        }

        /// <summary>
        /// Checks to see if a character is blocking the way
        /// </summary>
        /// <returns>true if no character is in the way, false otherwise</returns>
        private bool CharacterBlocked()
        {
            return enemy.CharacterBlocked();
        }
        #endregion
    }
}
