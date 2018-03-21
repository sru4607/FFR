using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class Player : Character, ICombat
    {
        public Player()
            :base()
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
        public void TakeDamage(int damageAmount)
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
        public void Die()
        {

        }

        //TODO: Method to stun the player for the amount of time chosen
        public void Stun(double timeToStun)
        {

        }
    }
}
