using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class NPC : Character
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor
        /// <summary>
        /// Creates a generic NPC with X and Y values
        /// </summary>
        /// <param name="x">X location of the NPC</param>
        /// <param name="y">Y location of the NPC</param>
        /// <param name="node">Reference to the QuadTree</param>
        public NPC(int x, int y, QuadTreeNode node)
            // Defaults to a width of 64 and a height of 128
            :base(x, y, 64, 128, node)
        {

        }
        #endregion

        #region Methods

        #endregion

        #region Update
        public override void Update()
            {

            }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch sb)
            {

            }
        #endregion

    }
}
