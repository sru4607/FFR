using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    public abstract class Character : PhysicsObject, ICombat
    {
        #region Fields
        protected int hp;
        protected Rectangle mBox;
        protected int mDamage;
        protected int rDamage;
        protected int stunFrames;
        protected bool right;
        #endregion

        #region Properties
        public virtual int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public virtual Rectangle MBox
        {
            get { return mBox; }
            set { mBox = value; }
        }
        public virtual bool Right
        {
            get { return right; }
            set { right = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a generic instance of the Character class
        /// </summary>
        /// <param name="x">X location of the Character</param>
        /// <param name="y">Y location of the Character</param>
        /// <param name="node">Reference to the quad tree used in-game</param>
        /// <param name="right">Whether the character starts facing right</param>
        public Character(int x, int y, QuadTreeNode node, bool right = true)
            // Defaults to a width of 64 and height of 128
            : base(x, y, 64, 128, node)
        {
            hp = 100;
            // TODO: Verify whether the hitbox line of code is valid or if it belongs in GameObject 
            // Also, should it use 0, 0, 32, 64; or x, y, 32, 64?
            mBox = new Rectangle((int)Position.X + (int)Size.X, (int)Position.Y + ((int)Size.Y / 6), (int)Size.X, (int)Size.Y * 2 / 3);
            mDamage = 1;
            rDamage = 0;
            stunFrames = 0;
            this.right = right;
        }

        /// <summary>
        /// Creates a Character with a specific width and height
        /// </summary>
        /// <param name="x">X position of the character</param>
        /// <param name="y">Y position of the character</param>
        /// <param name="width">Width of the character's draw hitbox</param>
        /// <param name="height">Height of the character's draw hitbox</param>
        /// <param name="node">Reference to the quad tree used in-game</param>
        /// <param name="right">True if the character faces right, else false</param>
        public Character(int x, int y, int width, int height, QuadTreeNode node, bool right = true)
            : base(x, y, width, height, node)
        {
            this.node = node;
            hp = 1;
            mBox = new Rectangle((int)Position.X + (int)Size.X, (int)Position.Y + (int)Size.Y / 6, (int)Size.X, (int)Size.Y * 2 / 3);

            Position = new Vector2(X, Y);
            
            mDamage = 1;
            rDamage = 0;
            stunFrames = 0;
            this.right = right;
        }
        #endregion

        #region Methods
        /// <summary>
        /// used to check if another character is hit by a melee attack
        /// </summary>
        public virtual void MAttack()
        {
            List<QuadTreeNode> parents = node.GetParents();
            for (int i = 0; i < parents.Count; i++)
            {
                for (int j = 0; j < parents[i].Objects.Count; j++)
                {
                    if (AttackIntersects(parents[i].Objects[j]) && parents[i].Objects[j] is Character)
                    {
                        Character temp = (Character) parents[i].Objects[j];
                        temp.TakeDamage(mDamage);
                    }
                }
            }
        }

        public virtual bool AttackIntersects(GameObject @object)
        {
            return mBox.Intersects(new Rectangle((int)@object.Position.X, (int)@object.Position.Y, (int)@object.Size.X, (int)@object.Size.Y));
        }

        /// <summary>
        /// the Character makes a ranged attack
        /// </summary>
        public virtual void RAttack()
        {
            //to be implemented later
        }


        public void Flip()
        {
            if (mBox.Location.X > X)
            {
                //Flip to left side
            } 
        }


        /// <summary>
        /// The Character dies
        /// </summary>
        public abstract void Die();

        /// <summary>
        /// The Character's hp reduces
        /// </summary>
        /// <param name="damageAmount">the amount of damage to take</param>
        public void TakeDamage(int damageAmount)
        {
            // This method won't ever be used. The method will be passed down to whatever the object is.
            return;
        }

        /// <summary>
        /// The character is stunned for a bit
        /// </summary>
        /// <param name="stunFrames">Number of frames that the character is stunned for</param>
        public void Stun(int stunFrames)
        {
            // This method won't ever be used. The method will be passed down to whatever the object is.
            return;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the status of a Character object
        /// </summary>
        public override void Update(GameTime gm)
            {
            if (right) { mBox.X = (int)Position.X + (int)Size.X; }
            else
            {
                mBox.X = (int)Position.X - (int)Size.X;
                if (this is Player) { mBox.X = (int)Position.X - ((int)Size.X / 2); }
            }
            mBox.Y = (int)Position.Y + (int)Size.Y / 6;
                base.Update(gm);
            }
        #endregion

        #region Draw
        /// <summary>
        /// Draws a Character object
        /// </summary>
        /// <param name="sb">the related spritebatch</param>
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
        #endregion
    }
}
