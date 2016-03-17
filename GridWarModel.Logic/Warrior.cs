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

        public void addWeapon(Weapon weapon)
        {
            if (this.weapon != null)
                throw new InvalidOperationException("Can't handle more than one weapon!. Player already has a weapon.");

            this.weapon = weapon;
        }

        public void dropWeapon()
        {
            if (this.weapon == null)
                throw new InvalidOperationException("Can't drop non-existent weapon!. Player does not have a weapon.");

            this.weapon = null;
        }

        // Does not consider orientation of a player, direction is based on the orientation of the board
        public void Move(Direction direction)
        {
            Board.Move(this, direction);
        }

        // Get attack type
        public abstract AttackType getAttackType();
    }
}
