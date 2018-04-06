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

        // Number of frames to follow the movement pattern
        int frameCounter;
        int walkLeft;
        int walkRight;
        int pauseLeft;
        int pauseRight;

        double walkSpeed;
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
        // TODO: Implement non-generic values as overloads

        /// <summary>
        /// Initializes a generic patrol pattern
        /// </summary>
        /// <param name="patrolType">Pattern of movement</param>
        public AI(PatrolType patrolType)
        {
            this.patrolType = patrolType;
            patrolState = PatrolState.PauseRight;
            facingRight = true;
            walkSpeed = 3.0;

            if(patrolType == PatrolType.Standing)
            {
                walkLeft = 0;
                walkRight = 0;
            }
            else if(patrolType == PatrolType.Moving)
            {
                walkLeft = 60;
                walkRight = 60;
            }

            // TODO: Make these adjustable
            pauseLeft = 120;
            pauseRight = 120;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the enemy's movement AI by 1 frame and returns the change in X position 
        /// Called through a class property
        /// </summary>
        public double MoveAI()
        {
            // TODO: Implement vertical movement into the return statement

            // Update the current state
            switch (patrolState)
            {
                // For the current finite state,
                case PatrolState.WalkLeft:
                    // If the end of the frame's cycle has been reached,
                    if (frameCounter >= walkLeft)
                    {
                        // Move to the next finite state and reset the counter
                        frameCounter = 0;
                        patrolState = PatrolState.PauseLeft;
                    }
                    // Otherwise, increase the frame count by one
                    else { frameCounter++; }
                    break;

                case PatrolState.PauseLeft:
                    if (frameCounter >= pauseLeft)
                    {
                        frameCounter = 0;
                        patrolState = PatrolState.WalkRight;
                        facingRight = true;
                    }
                    else { frameCounter++;  }
                    break;

                case PatrolState.WalkRight:
                    if (frameCounter >= walkRight)
                    {
                        frameCounter = 0;
                        patrolState = PatrolState.PauseRight;
                    }
                    else { frameCounter++; }
                    break;


                case PatrolState.PauseRight:
                    if (frameCounter >= pauseRight)
                    {
                        frameCounter = 0;
                        patrolState = PatrolState.WalkLeft;
                        facingRight = false;
                    }
                    else { frameCounter++; }
                    break;
            }

            // Move based off the current state
            switch (patrolState)
            {
                // Return the change in movement
                case PatrolState.WalkLeft:
                    return -walkSpeed;
                case PatrolState.PauseLeft:
                    return 0;
                case PatrolState.WalkRight:
                    return walkSpeed;
                case PatrolState.PauseRight:
                    return 0;
                default:
                    throw new NotImplementedException("Unknown patrol state in AI.UpdateMovement()");
            }
        }
        #endregion
    }
}
