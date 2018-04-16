using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualGame
{
    class Tile : GameObject
    {
        //To Be implemented
        #region Fields
        public bool Solid { get; set; }
        public int Depth { get; set; }
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public Tile(Texture2D texture, int depth)
        {
            this.texture = texture;
            if (texture != null)
                noClip = true;
            Depth = depth;
        }
        #endregion

        #region Methods

        #endregion

        #region Update

        #endregion

        #region Draw
        public override void Draw(SpriteBatch sb)
        {
            if (texture != null)
                base.Draw(sb);
        }
        #endregion
    }
}
