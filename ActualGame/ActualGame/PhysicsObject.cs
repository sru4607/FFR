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
        public Vector2 Movement { get; set; }
        private Vector2 prevLocation;

        public PhysicsObject()
        {

        }

        public void KeyboardMovement()
        {
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Left)) { Movement += 0.5f * Vector2.UnitX; }
            if (kb.IsKeyDown(Keys.Right)) { Movement += -0.5f * Vector2.UnitX; }
            if (kb.IsKeyDown(Keys.Space)) { Jump(); }
            
        }

        private void Gravity()
        {
            Movement += Vector2.UnitY * 0.5f;
        }

        private void Friction()
        {

        }

        private void MoveAsPossible(GameTime gm)
        {
            prevLocation = new Vector2(Rect.X, Rect.Y);
            UpdatePosition(gm);

        }

        private void UpdatePosition(GameTime gm)
        {

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

        }
    }
}
