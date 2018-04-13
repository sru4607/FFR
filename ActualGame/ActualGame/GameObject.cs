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
        public Vector2 Movement { get; set; }
        public Vector2 Position { get; set; }
        private Vector2 oldPosition;
        protected Rectangle rect;
        protected double velX;
        protected double velY;
        protected Texture2D texture;
        protected Rectangle prev;
        protected int step;
        static float grav = 9.8f;
        protected bool physicsObject = false;
        public bool noClip = false;
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
        public bool Finished
        {
            get { return step >= 15; }
        }

        /// <summary>
        /// Get and set for the X value of the game object hitbox's top-left corner
        /// </summary>
        public int X
        {
            get { return rect.X; }
            set { rect = new Rectangle(value, Rect.Y, Rect.Width, Rect.Height); }
        }

        /// <summary>
        /// Get and set for the Y value of the game object hitbox's top-left corner
        /// </summary>
        public int Y
        {
            get { return rect.Y; }
            set { rect = new Rectangle(Rect.X, value, Rect.Width, Rect.Height); }
        }

        /// <summary>
        /// Get and set for the width of the game object hitbox
        /// </summary>
        public int Width
        {
            get { return rect.Width; }
            set { rect = new Rectangle(Rect.X, Rect.Y, value, Rect.Height); }
        }

        /// <summary>
        /// Get and set for the height of the game object hitbox
        /// </summary>
        public int Height
        {
            get { return rect.Height; }
            set { rect = new Rectangle(Rect.X, Rect.Y, Rect.Width, value); }
        }

        /// <summary>
        /// Get and set for the entirety of the hitbox as a Rectangle
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        /// <summary>
        /// Get and set for whether the object experiences physics (most notably, gravity)
        /// </summary>
        public bool Physics
        {
            get { return physicsObject; }
            set { physicsObject = value; }
        }
        #endregion

        #region Constructor
        // TODO: Update Constructor fields (possibly have GameObject() take parameters)
        public GameObject()
        {
            rect = new Rectangle();
            velX = 0.0;
            velY = 0.0;
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
        /// <summary>
        /// 
        /// </summary>
        public void Move()
        {
            prev = rect;
            step = 0;
            X = (int)(X + velX);
            Y = (int)(Y + velY);
        }

        public void Move(bool right)
        {
            prev = rect;
            step = 0;
            if (right)
            {
                X = (int)(X + velX);
            }
            else
            {
                X = (int)(X - velX);
            }
            Y = (int)(Y + velY);
            /* check for collisions with the wall, if colliding, revert to previous state and set speed to 0 */
        }

        private void AffectWithGravity()
        {
            Movement += Vector2.UnitY * .65f;
        }

        private void SimulateFriction()
        {
            if (IsOnFirmGround()) { Movement -= Movement * Vector2.One * .08f; }
            else { Movement -= Movement * Vector2.One * .02f; }
        }

        private void MoveAsFarAsPossible(GameTime gameTime)
        {
            oldPosition = Position;
            UpdatePositionBasedOnMovement(gameTime);
            Position = World.CurrentBoard.WhereCanIGetTo(oldPosition, Position, rect);
        }

        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
        }

        public bool IsOnFirmGround()
        {
            Rectangle onePixelLower = rect;
            onePixelLower.Offset(0, 1);
            return !World.CurrentBoard.HasRoomForRectangle(onePixelLower);
        }

        private void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Position - oldPosition;
            if (lastMovement.X == 0) { Movement *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { Movement *= Vector2.UnitX; }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Collision(List<GameObject> temp)
        {
            for(int i = 0; i< temp.Count; i++)
            {
                if(this != temp[i] && !temp[i].noClip)
                {
                    if (this.rect.Intersects(temp[i].Rect))
                    {
                        //this.Revert();
                        velY = 0;
                    }
                        
                    
                }
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        virtual public void Update(GameTime gameTime)
        {
            if (this is Player temp)
                temp.Update(gameTime);
            AffectWithGravity();
            SimulateFriction();
            MoveAsFarAsPossible(gameTime);
            StopMovingIfBlocked();

        }
        #endregion

        #region Draw
        /// <summary>
        /// Draws the object to the screen based off rect's parameters
        /// </summary>
        /// <param name="sb"></param>
        virtual public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
        #endregion
    }
}
