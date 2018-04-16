using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class Character : PhysicsObject, ICombat
    {
        #region Fields
        protected int hp;
        protected Rectangle mBox;
        protected Rectangle hitBox;
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
            hp = 1;
            hitbox = new Rectangle(0, 0, 32, 64);
            mBox = new Rectangle(HitBox.Location.X + 32, HitBox.Location.Y, 32, 56);
            mDamage = 0;
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
            mBox = new Rectangle(rect.Location.X + 32, rect.Location.Y, 32, 56);
            mDamage = 0;
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
                    if (mBox.Intersects(parents[i].Objects[j].Rect) && parents[i].Objects[j] is Character)
                    {
                        Character temp = (Character) parents[i].Objects[j];
                        temp.TakeDamage(mDamage);
                    }
                }
            }
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
            if (mBox.Location.X > hitbox.Location.X)
            {
                //Flip to left side
            } 
        }


        /// <summary>
        /// The Character dies
        /// </summary>
        public void Die()
        {
            // This method won't ever be used. The method will be passed down to whatever the object is.
            return;
        }

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
        public override void Update()
            {
                base.Update();
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
