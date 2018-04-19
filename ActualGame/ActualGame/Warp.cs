using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualGame
{
    class Warp : GameObject
    {
        #region Fields
        private string destinationMap;
        private int xOffset;
        private int yOffset;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public Warp(int x, int y, string destinationMap, int xOffset, int yOffset, QuadTreeNode node)
            :base(x, y, 64, 64, node)
        {
            this.destinationMap = destinationMap;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }
        #endregion

        #region Methods
        
        #endregion

        #region Update

        #endregion
    }
}
