using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public string Destination { get; set; }
        private int xOffset;
        private int yOffset;
        #endregion

        #region Properties
        public Vector2 DestinationPosition
        {
            get { return new Vector2(xOffset, yOffset); }
        }
        #endregion

        #region Constructor
        public Warp(int x, int y, string destinationMap, int xOffset, int yOffset, QuadTreeNode node)
            :base(x, y, 64, 64, node)
        {
            Destination = destinationMap;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            noClip = true;
        }
        #endregion

        #region Methods

        #endregion

        #region Update

        #endregion
    }
}
