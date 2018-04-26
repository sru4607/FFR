using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualGame
{
    class Boss : Character
    {
        public Boss()
        :base(0,0,null)
        {

        }


        public override void Die()
        {
            throw new NotImplementedException();
        }

    }
}
