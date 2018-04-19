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
    public class GameObject
    {
        #region Fields
        // Fields
        public Vector2 position;
        public Vector2 size;
        public Texture2D texture;
        public bool noClip = false;
        protected QuadTreeNode node;
        #endregion

        #region Properties
        /// <summary>
        /// Get and set for the GameObject's 2D position in the world
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Get and set for the width/height of the GameObject
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Get and set for the texture of the object
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// Get and set for whether the game object can collide with the player
        /// </summary>
        public bool NoClip
        {
            get { return noClip; }
            set { noClip = value; }
        }
        
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
        /// Method used to load the texture in Game1.LoadContent()
        /// </summary>
        /// <param name="texture">Content.Load&lt;Texture2D&gt;("INSERTLOCATION")</param>
        public void LoadTexture(Texture2D texture)
        {
            this.Texture = texture;
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
            sb.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, Vector2.Zero, new Vector2(Width / Texture.Width, Height / Texture.Height), SpriteEffects.None, 0);
        }
        #endregion
    }
}
