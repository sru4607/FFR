using Microsoft.Xna.Framework;
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
        private Texture2D backTexture;
        public bool Solid { get; set; }
        public int Depth { get; set; }
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public Tile(Texture2D texture, Texture2D backTexture, int depth)
        {
            this.backTexture = backTexture;
            Texture = texture;
            if (depth == 0)
                noClip = false;
            else
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
            if (World.Current.Name != "BossMap" )
                sb.Draw(backTexture, Position, new Rectangle(0, 0, backTexture.Width, backTexture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / backTexture.Width, Height / backTexture.Height), SpriteEffects.None, 0);
            if (Texture != null)
                base.Draw(sb);
        }
        #endregion
    }
}
