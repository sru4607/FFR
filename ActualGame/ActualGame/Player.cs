using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    //Enumeration for the FSM for the Player actions
    public enum PlayerState { Walk, Jump, Idle, MAttack}

    class Player : Character, ICombat
    {
        /// <summary>
        /// Creates a new instance of the Player class
        /// </summary>
        /// <param name="hurtBox">The hitbox for incoming damage</param>
        /// <param name="mBox">The hitbox of outgoing attacks (X, Y is localized from the center of the hitbox)</param>
        /// <param name="drawBox">The Rectangle that will be used to draw the player (X, Y will be auto-updated by hurtBox)</param>
        /// <param name="hp">Amount of health the character has</param>
        /// <param name="melee">True if the character is melee-based, false if the character is ranged</param>
        /// <param name="damage">The amount of damage each attack does</param>
        public Player(Rectangle hurtBox, Rectangle mBox, Rectangle drawBox, int hp, bool melee, int damage)
            :base(hurtBox, mBox, drawBox, hp, melee, damage)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        /// <summary>
        /// Reduces the health of the Player when they take damage, and calls the Die method if necessary
        /// </summary>
        /// <param name="damageAmount">The amount of damage taken</param>
        public new void TakeDamage(int damageAmount)
        {
            if (damageAmount > hp)
            {
                hp = 0;
                Die();
            }
            else
                hp -= damageAmount;
        }

        //TODO: Method to call that should update the game to signal the Player has died
        public new void Die()
        {

        }

        //TODO: Method to stun the player for the amount of time chosen
        public new void Stun(int stunFrames)
        {
            
        }
    }
}
