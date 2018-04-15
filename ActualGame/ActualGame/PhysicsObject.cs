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
    class PhysicsObject : GameObject
    {
        //Thanks for help from XNAFan's Blog for some physics code guides
        public Vector2 Movement { get; set; }
        private Vector2 prevLocation;

        public PhysicsObject()
        {
           
        }

        public override void Update(GameTime gm)
        {
                KeyboardMovement();
            Gravity();
            Friction();
            MoveAsPossible(gm);
            StopIfBlocked();
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        public void KeyboardMovement()
        {
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Left)) { Movement = -0.5f * Vector2.UnitX; }
            if (kb.IsKeyDown(Keys.Right)) { Movement = Vector2.UnitX * 0.5f; }
            if (kb.IsKeyDown(Keys.Space)) { Jump(); }
            
        }

        private void Gravity()
        {
            Movement += Vector2.UnitY;
        }

        private void Friction()
        {
            if (OnGround())
                Movement = 0.8f * Movement;
            else
                Movement = 0.9f * Movement;
        }

        private void MoveAsPossible(GameTime gm)
        {
            prevLocation = new Vector2(Rect.X, Rect.Y);
            UpdatePosition(gm);
            Position = World.Current.WhereCanIGetTo(prevLocation, Position, Rect);
            Rect = new Rectangle((int)Position.X, (int)Position.Y, Rect.Width, Rect.Height);

        }

        private void UpdatePosition(GameTime gm)
        {
            Position += (Movement * (float)gm.ElapsedGameTime.TotalMilliseconds / 15);
        }

        public bool OnGround()
        {
            Rectangle lower = Rect;
            lower.Offset(0, 1);
            return false;
        }

        private void Jump()
        {
            if(OnGround())
                Movement -= Vector2.UnitY * 2f;
        }

        private void StopIfBlocked()
        {
            Vector2 diff = prevLocation - Position;
            if(diff.X == 0)
            {
                Movement *= Vector2.UnitY;
            }
            if(diff.Y == 0)
            {
                Movement *= Vector2.UnitX;
            }
        }
    }
}
