﻿using System;
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
        protected int stunFrames;

        //Properties
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
        public virtual Rectangle HurtBox
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
            stunFrames = 0;
        }

        /// <summary>
        /// used to check if another character is hit by a melee attack
        /// </summary>
        public virtual void MAttack(Character c)
        {
            if (mBox.Intersects(c.hurtBox))
            {
                c.TakeDamage(mDamage);
            }
        }

        /// <summary>
        /// the Character makes a ranged attack
        /// </summary>
        public virtual void RAttack()
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

        /// <summary>
        /// The Character dies
        /// </summary>
        public void Die()
        {
            // This method won't ever be used. The method will be passed down to whatever the object is.
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
        }
    }
}
