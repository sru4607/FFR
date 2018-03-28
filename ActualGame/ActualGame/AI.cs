using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PatrolType { Standing, Moving }
enum PatrolState { MoveLeft, MoveRight, PauseLeft, PauseRight }
namespace ActualGame
{
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
        int direction; // 0 is facing left, 1 is facing right
        #endregion

        #region Properties

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a generic patrol pattern
        /// </summary>
        /// <param name="patrolType">Pattern of movement</param>
        public AI(PatrolType patrolType)
        {
            this.patrolType = patrolType;
            patrolState = PatrolState.PauseRight;
            direction = 1;

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
        }
        #endregion

        #region Methods

        #endregion

        #region Draw

        #endregion
    }
}
