using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ActualGame
{
    class NPC : Character
    {
        /// <summary>
        /// Creates a new instance of the Character class
        /// </summary>
        /// <param name="hurtBox">The hitbox for incoming damage</param>
        /// <param name="mBox">The hitbox of outgoing attacks (X, Y is localized from the center of the hitbox)</param>
        /// <param name="drawBox">The Rectangle that will be used to draw the player (X, Y will be auto-updated by hurtBox)</param>
        /// <param name="hp">Amount of health the character has</param>
        /// <param name="melee">True if the character is melee-based, false if the character is ranged</param>
        /// <param name="damage">The amount of damage each attack does</param>
        public NPC(Rectangle hurtBox, Rectangle mBox, Rectangle drawBox, int hp, bool melee, int damage)
            :base(hurtBox, mBox, drawBox, hp, melee, damage)
        {
            // TODO: remove damage-related hitboxes
        }
        public override void Update()
        {

        }
        public override void Draw(SpriteBatch sb)
        {

        }
    }
}
