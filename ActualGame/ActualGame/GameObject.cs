using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ActualGame
{
    class GameObject
    {
        #region Fields
        // Fields
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        protected Texture2D texture;
        public bool noClip = false;
        protected QuadTreeNode node;
        #endregion

        #region Properties
        /// <summary>
        /// Returns whether the game object can collide with the player
        /// </summary>
        public bool NoClip
        {
            get { return noClip; }
            set { noClip = value; }
        }

        // TODO: Add description
        /// <summary>
        /// Get and set for the X value of the game object hitbox's top-left corner
        /// </summary>
        public float X
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Position.Y); }
        }

        /// <summary>
        /// Get and set for the Y value of the game object hitbox's top-left corner
        /// </summary>
        public float Y
        {
            get { return Position.Y; }
            set { Position = new Vector2(Position.X, value); }
        }

        /// <summary>
        /// Get and set for the width of the game object hitbox
        /// </summary>
        public float Width
        {
            get { return Size.X; }
            set { Size = new Vector2(value, Size.Y); }
        }

        /// <summary>
        /// Get and set for the height of the game object hitbox
        /// </summary>
        public float Height
        {
            get { return Size.Y; }
            set { Size = new Vector2(Size.X, value); }
        }


        #endregion

        #region Constructor
        /// <summary>
        /// Generic GameObject without parameters
        /// </summary>
        public GameObject()
        {
            Position = new Vector2(0,0);
        }

        /// <summary>
        /// Creates a generic GameObject with the provided Rectangle dimensions
        /// </summary>
        /// <param name="x">X dimension of the draw box</param>
        /// <param name="y">Y dimension of the draw box</param>
        /// <param name="width">Width of the draw box</param>
        /// <param name="height">Height of the draw box</param>
        public GameObject(int x, int y, int width, int height, QuadTreeNode node)
        {
            Position = new Vector2(x, y); 
            Size = new Vector2(width, height);
            this.node = node;
            node.AddObject(this);
        }
        #endregion

        #region LoadTexture
        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture">Content.Load&lt;Texture2D&gt;("INSERTLOCATION")</param>
        public void LoadTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        #endregion

        #region Methods
        
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        virtual public void Update(GameTime gm)
        {
            node = node.GetContainingQuad(this);
            if (this is PhysicsObject temp)
                ((PhysicsObject)temp).Update(gm);

        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the object to the screen based off rect's parameters
        /// </summary>
        /// <param name="sb"></param>
        virtual public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, Position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / texture.Width, Height / texture.Height), SpriteEffects.None, 0);
        }
        #endregion
    }
}
