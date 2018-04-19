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
        public Vector2 prevLocation { get; set; }

        //just use the gameobject constructor
        public PhysicsObject()
            : base()
        {
           
        }
        //just use the gameObject parameterized constructor
        public PhysicsObject(int x, int y, int width, int height, QuadTreeNode node)
            :base(x, y, width, height, node)
        {

        }

        //Adjust movement based on physics
        public override void Update(GameTime gm)
        {
            
            Gravity();
            Friction();
            MoveAsPossible(gm);
            StopIfBlocked();
        }
        //draw the object
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
        //apply gravity
        private void Gravity()
        {
            Movement += Vector2.UnitY;
        }
        //if onGround apply more friction
        private void Friction()
        {
            if (OnGround())
                Movement -= 0.08f * Movement;
            else
                Movement -= 0.02f * Movement;
        }
        //Move based on gameTime and movement
        private void MoveAsPossible(GameTime gm)
        {
            prevLocation = Position;
            UpdatePosition(gm);
            Position = World.Current.WhereCanIGetTo(this, prevLocation, Position, new Rectangle((int)Position.X, (int)Position.Y,(int)Size.X,(int)Size.Y));  
        }
        //adjust the position field based on Game Time
        private void UpdatePosition(GameTime gm)
        {
            Position += (Movement * (float)gm.ElapsedGameTime.TotalMilliseconds / 15);
        }
        //check if you are on the ground
        public bool OnGround(int distExtra = 0)
        {
            Rectangle Lower = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Lower.Offset(distExtra, 2);
            return (World.Current.HasRoomForRectangle(Lower, this));
           

        }
        //check if you are at the edge of the platform
        public bool AtEdge(int distExtra = 0)
        {
            Rectangle Right = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Right.Offset(distExtra, 1);
            Rectangle Left = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Left.Offset(-distExtra, 1);
            if (World.Current.HasRoomForRectangle(Right, null))
            {
                return true;
            }
            if (World.Current.HasRoomForRectangle(Left, null))
            {
                return true;
            }
            return false;

        }
        //check if you are at the wall
        public bool AtWall(int distExtra = 1)
        {
            Rectangle Right = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Right.Offset(distExtra, 0);
            if(World.Current.HasRoomForRectangle(Right, null))
            {
                return true;   
            }
            Rectangle Left = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Left.Offset(-distExtra, 0);
            if (World.Current.HasRoomForRectangle(Left, null))
            {
                return true;
            }
            return false;

        }

        //if blocked take away velocity
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
