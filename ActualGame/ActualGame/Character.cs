using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class Character : GameObject, ICombat
    {
        //Fields
        protected int hp;
        protected Rectangle mBox;
        protected Rectangle hurtBox;
        protected int mDamage;
        protected int rDamage;

        //Properties
        protected int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        protected Rectangle MBox
        {
            get { return mBox; }
            set { mBox = value; }
        }
        protected Rectangle HurtBox
        {
            get { return hurtBox; }
            set { hurtBox = value; }
        }

        /// <summary>
        /// Creates a new instance of the Character class
        /// </summary>
        public Character()
            :base()
        {
            hp = 1;
            mBox = new Rectangle();
            hurtBox = new Rectangle();
            mDamage = 0;
            rDamage = 0;
        }

        /// <summary>
        /// used to check if another character is hit by a melee attack
        /// </summary>
        public void MAttack(Character c)
        {
            if (mBox.Intersects(c.hurtBox))
            {
                c.TakeDamage(mDamage);
            }
        }

        /// <summary>
        /// the Character makes a ranged attack
        /// </summary>
        public void RAttack()
        {
            //to be implemented later
        }

        /// <summary>
        /// Updates the status of a Character object
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// Draws a Character object
        /// </summary>
        /// <param name="sb">the related spritebatch</param>
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        public void TakeDamage(int damageAmount)
        {
            hp -= damageAmount;
        }

        public void Die()
        {
            return;
        }

        public void Stun(double timeToStun)
        {
            return;
        }
    }
}
