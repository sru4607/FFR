using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

enum FSM { Docile, Search, Aggro, Damaged, Attack }
namespace ActualGame
{
    class Enemy : Character, ICombat
    {
        // Fields
        AI mainAi;

        /// <summary>
        /// Creates a generic Enemy
        /// </summary>
        public Enemy()
            : base()
        {
            mainAi = new AI();
        }

        public override void Update()
        {
            // TODO Implement AI movement

            base.Update();
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        /*
        /// <summary>
        /// Determines whether an object on the screen is colliding with the enemy
        /// </summary>
        /// <param name="hitboxes">List of objects from the quad tree</param>
        /// <returns>True if collision is present, else returns false</returns>
        public bool IsHit(List<GameObject> objects)
        {
            // TODO Ensure a quad tree *and its children* are used
            foreach (GameObject obj in objects)
            {
                if (Rect.Intersects(obj.Rect))
                {
                    // TODO Implement method of selectively ignoring collision (e.g. other enemies)
                    return true;
                }
            }
            return false;
        }
        
         Is this really necessary? Because GameObject should handle collision*/
        
        // TODO Implement ICombat TakeDamage() into combat collision detection method

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


        public new void Die()
        {

        }


        public new void Stun(int stunFrames)
        {

        }
    }
}
