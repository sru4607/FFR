using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

enum EnemyState { Docile, Search, Aggro, Damaged, Attack }
namespace ActualGame
{
    class Enemy : Character, ICombat
    {
        #region Fields
        AI mainAi;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        /// <summary>
        /// Creates a generic Enemy
        /// </summary>
        public Enemy()
            : base()
        {
            mainAi = new AI(PatrolType.Standing);
        }
        #endregion

        #region Methods
        public new void TakeDamage(int damageAmount)
        {
            if (damageAmount >= hp)
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
        #endregion

        #region Update
        public override void Update()
        {
            // TODO Implement AI movement

            base.Update();
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
        #endregion
    }
}
