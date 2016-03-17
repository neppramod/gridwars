using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class Weapon
    {
        private const int MIN_STAFF_POWER = 3;
        private const int MAX_STAFF_POWER = 6;
        private const int MIN_SWORD_POWER = 2;
        private const int MAX_SWORD_POWER = 4;

        public WeaponType WeaponType
        {
            get;
        }

        public int Power { get;}

        public Weapon(WeaponType weaponType)
        {
            this.WeaponType = weaponType;
            this.Power = this.WeaponType == WeaponType.Staff ? 
                Util.random.Next(MIN_STAFF_POWER, MAX_STAFF_POWER + 1) : Util.random.Next(MIN_SWORD_POWER, MAX_SWORD_POWER + 1);
        }
    }
}
