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
        protected Rectangle mBox; // Melee attack box
        protected Rectangle hurtBox; // Hitbox for incoming damage
        protected Rectangle drawBox; // The size of the character to draw to the screen
        protected int mDamage;
        protected int rDamage;
        protected bool melee;
        protected int stunFrames;

        /// <summary>
        /// Creates a new instance of the Character class
        /// </summary>
        /// <param name="hurtBox">The hitbox for incoming damage</param>
        /// <param name="mBox">The hitbox of outgoing attacks (X, Y is localized from the center of the hitbox)</param>
        /// <param name="drawBox">The Rectangle that will be used to draw the player (X, Y will be auto-updated by hurtBox)</param>
        /// <param name="hp">Amount of health the character has</param>
        /// <param name="melee">True if the character is melee-based, false if the character is ranged</param>
        /// <param name="damage">The amount of damage each attack does</param>
        public Character(Rectangle hurtBox, Rectangle mBox, Rectangle drawBox, int hp, bool melee, int damage)
            : base()
        {
            // Save character-specific constants
            this.hp = hp;
            this.mBox = mBox;
            this.hurtBox = hurtBox;
            this.drawBox = drawBox;
            this.melee = melee;

            // Sets the type of damage
            if(melee)
                mDamage = damage;
            else
                rDamage = damage;

            // Initialize internal variables
            stunFrames = 0;
        }

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
            // TODO: Make the drawable hitbox match the player's location (How will it be mapped? Top-left corner? Centered? etc.)
            base.Draw(sb);
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
    }
}
