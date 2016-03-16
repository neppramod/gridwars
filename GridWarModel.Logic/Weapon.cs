using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class Weapon
    {
        public WeaponType WeaponType
        {
            get;
        }

        public int Power { get;}

        public Weapon(WeaponType weaponType)
        {
            this.WeaponType = weaponType;
            this.Power = this.WeaponType == WeaponType.Staff ? Util.random.Next(3, 7) : Util.random.Next(2, 5);
        }
    }
}
