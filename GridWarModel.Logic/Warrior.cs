using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public abstract class Warrior
    {

        public const int MIN_MELEE_POWER = 1;
        public const int MAX_MELEE_POWER = 20;
        public const int MIN_MAGIC_POWER = 1;
        public const int MAX_MAGIC_POWER = 15;
        public const int MIN_DEFENSE_PERCENTAGE = 0;
        public const int MAX_DEFENSE_PERCENTAGE = 100;

        public int HitPoints { get; protected set; }
        public int MeleePower {get; set;}
        public int MagicPower { get; set;}
        public int DefensePercentage { get; protected set;}

        public  int Id { get; set; }        

        public PlayerType Player { get; set; }

        Weapon weapon;
        public Position Position { get; set; }

        // All warriors start with hit points 100 and defense percentage of 0%
        public Warrior()
        {
            this.HitPoints = 100;
            this.DefensePercentage = 0;
        }

        public bool hasWeapon()
        {
            return weapon == null ? false : true;
        }

        public bool addWeapon(Weapon weapon)
        {
            if (this.weapon == null)
            {
                this.weapon = weapon;

                // Increase warrior's power by weapon power
                if (weapon.WeaponType == WeaponType.Sword)
                    this.MeleePower += this.weapon.Power;
                else if (weapon.WeaponType == WeaponType.Staff)
                    this.MagicPower += this.weapon.Power;
                return true;
            }

            return false;
        }

        public void dropWeapon()
        {
            if (this.weapon != null)
                this.weapon = null;
        }

        // Get attack type
        public abstract AttackType getAttackType();
    }
}
