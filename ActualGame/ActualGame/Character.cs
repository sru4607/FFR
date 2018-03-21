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
        protected int stunFrames;

        //Properties
        protected int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        /// <summary>
        /// Creates a new instance of the Character class
        /// </summary>
        public Character()
            :base()
        {
            hp = 1;
            mBox = new Rectangle();
        }

        /// <summary>
        /// the Character makes a melee attack
        /// </summary>
        public void MAttack()
        {
            
        }

        /// <summary>
        /// the Character makes a ranged attack
        /// </summary>
        public void RAttack()
        {

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

        }

        /// <summary>
        /// The Character's hp reduces
        /// </summary>
        /// <param name="damageAmount">the amount of damage to take</param>
        public void TakeDamage(int damageAmount)
        {
            // TODO: Add conditions on when the enemy can take damage
            if (damageAmount > 0 && true)
            {
                hp -= damageAmount;

                if (hp <= 0) Die();
                else Stun(5);
            }
        }

        /// <summary>
        /// The character is stunned for a bit
        /// </summary>
        /// <param name="stunFrames">Number of frames that the character is stunned for</param>
        public void Stun(int stunFrames)
        {
            // May require more - possibly finite state change?
            this.stunFrames = stunFrames;
        }
    }
}
