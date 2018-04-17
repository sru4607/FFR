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
            : base()
        {
           
        }

        public PhysicsObject(int x, int y, int width, int height, QuadTreeNode node)
            :base(x, y, width, height, node)
        {

        }

        public override void Update(GameTime gm)
        {
            
            Gravity();
            Friction();
            MoveAsPossible(gm);
            StopIfBlocked();
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        private void Gravity()
        {
            Movement += Vector2.UnitY;
        }

        private void Friction()
        {
            if (OnGround())
                Movement -= 0.08f * Movement;
            else
                Movement -= 0.02f * Movement;
        }

        private void MoveAsPossible(GameTime gm)
        {
            prevLocation = Position;
            UpdatePosition(gm);
            Position = World.Current.WhereCanIGetTo(this, prevLocation, Position, new Rectangle((int)Position.X, (int)Position.Y,(int)Size.X,(int)Size.Y));
             
        }

        private void UpdatePosition(GameTime gm)
        {
            Position += (Movement * (float)gm.ElapsedGameTime.TotalMilliseconds / 15);
        }

        public bool OnGround(int distExtra = 0)
        {
            Rectangle Lower = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Lower.Offset(distExtra, 2);
            return (World.Current.HasRoomForRectangle(Lower, this));
           

        }
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
