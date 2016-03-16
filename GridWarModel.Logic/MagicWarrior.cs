using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class MagicWorrior : Warrior
    {
        /// <summary>
        /// Magic worriors start with melee power in the range 5 to 10 and magic power in the range of 1 to 3
        /// </summary>
        public MagicWorrior()
        {
            this.MeleePower = Util.random.Next(5, 11);
            this.MagicPower = Util.random.Next(1, 4);
        }

        public override AttackType getAttackType()
        {
            throw new NotImplementedException();
        }
    }
}
