using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class MagicWarrior : Warrior
    {
        private const int MIN_MELEE_START_POWER = 1;
        private const int MAX_MELEE_START_POWER = 3;
        private const int MIN_MAGIC_START_POWER = 3;
        private const int MAX_MAGIC_START_POWER = 8;
        /// <summary>
        /// Magic worriors start with melee power in the range 5 to 10 and magic power in the range of 1 to 3
        /// </summary>
        public MagicWarrior()
        {
            this.MeleePower = Util.random.Next(MIN_MELEE_START_POWER, MAX_MELEE_START_POWER + 1);
            this.MagicPower = Util.random.Next(MIN_MAGIC_START_POWER, MAX_MAGIC_START_POWER + 1);
        }

        public override AttackType getAttackType()
        {
            throw new NotImplementedException();
        }
    }
}
