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
        }

        /// <summary>
        /// Initializes a more specific patrol pattern
        /// </summary>
        /// <param name="enemy">Reference to the enemy that the AI is controlling</param>
        /// <param name="patrolType">Pattern of movement</param>
        /// <param name="walkSpeed">The distance traveled in one frame of movement</param>
        /// <param name="pauseLeft"></param>
        /// <param name="pauseRight"></param>
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
            // TODO: Implement vertical movement into the return statement
            // TODO: Have the method update enemy.rect's location
            // TODO: Have the movement update based on collision hitbox detection (possibly vertices or on a grid)
            // Update the current state
            switch (patrolState)
            {
                case PatrolState.WalkLeft:
                    /*
                    if (frameCounter >= 999999)
                    {
                        frameCounter = 0;
                        patrolState = PatrolState.PauseLeft;
                    }
                    else { frameCounter++; }
                    break;

                case PatrolState.PauseLeft:
                    if (frameCounter >= 999999)
                    {
                        frameCounter = 0;
                        patrolState = PatrolState.WalkRight;
                        facingRight = true;
                    }
                    else { frameCounter++;  }
                    break;

                case PatrolState.WalkRight:
                    if (frameCounter >= 999999)
                    {
                        frameCounter = 0;
                        patrolState = PatrolState.PauseRight;
                    }
                    else { frameCounter++; }
                    break;


                case PatrolState.PauseRight:
                    if (frameCounter >= 999999)
                    {
                        frameCounter = 0;
                        patrolState = PatrolState.WalkLeft;
                        facingRight = false;
                    }
                    else { frameCounter++; }
                    break;
                    */
                default:
                    // Not implemented yet
                    break;
            }

            // Move based off the current state
            switch (patrolState)
            {
                /*
                case PatrolState.WalkLeft:
                    return -walkSpeed;
                case PatrolState.PauseLeft:
                    return 0;
                case PatrolState.WalkRight:
                    return walkSpeed;
                case PatrolState.PauseRight:
                    return 0;
                    */
                default:
                    // Not implemented yet
                    break;
            }
        }

        /// <summary>
        /// Helper method with determining whether there is an area to walk forward to
        /// </summary>
        /// <returns>True if the enemy can walk forward, else false</returns>
        private bool AbleToMove()
        {
            if (enemy.AtEdge(enemy.Texture.Width) || enemy.AtWall(enemy.Texture.Width))
                return false;
            return true;
        }
        #endregion
    }
}
