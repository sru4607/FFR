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
            // Update the current finite state
            switch(enemy.State)
            {
                case EnemyState.Docile:
                    // Check to see whether something causes the enemy to leave a Docile state
                    enemy.State = UpdateAggro();

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
                            break;
                        case PatrolState.WalkLeft:
                            // TODO: Add movement for walking left
                            break;
                        case PatrolState.WalkRight:
                            // TODO: Add movement for walking right
                            break;
                    }
                    break;
                // TODO: Add Search, Aggro, Damaged, and Attack movements
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
            if (enemy.AtEdge(enemy.Texture.Width) || enemy.AtWall(enemy.Texture.Width))
                return false;
            return true;
        }
        #endregion
    }
}
