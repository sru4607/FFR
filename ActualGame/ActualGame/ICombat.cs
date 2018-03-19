using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualGame
{
    interface ICombat
    {
        //A method for any Character to take damage that should subtract it from their health
        void TakeDamage(int damageAmount);

        //A method to kill the Character object if their health drops to (or below) zero
        void Die();

        //A method to stun a Character for a specific amount of time if they take damage
        void Stun(double timeToStun);
    }
}
