using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class Tile
    {
        //To Be implemented
        #region Fields
        public bool IsBlocked { get; set; }
        Vector2 position;
        Texture2D texture;
        SpriteBatch sb;
        #endregion

        #region Properties
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
        #endregion

        #region Constructor
        public Tile(Texture2D text, Vector2 pos, SpriteBatch spriteB)
        {
            texture = text;
            position = pos;
            sb = spriteB;
        }
        #endregion

        #region Methods

        #endregion

        #region Update

        #endregion

        #region Draw
        public void Draw()
        {

        }
        #endregion
    }
}
