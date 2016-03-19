using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class GameUtil
    {       
        public const int WARRIOR_SIZE = 6;
        public const int PLAYER_COUNT = 2;
        public static List<Warrior> allWarriors = new List<Warrior>();

        public List<Warrior> getWarriorsForAPlayer(PlayerType playerType)
        {
            return allWarriors.FindAll(w => w.Player == playerType);
        }

        public int getWarriorsCountForAPlayer(PlayerType playerType)
        {
            return getWarriorsForAPlayer(playerType).Count;
        }

        public void addWarrior(Warrior warrior)
        {
            allWarriors.Add(warrior);
        }

        public void deleteWarrior(Warrior warrior)
        {
            allWarriors.Remove(warrior);
        }

        public void switchTurn()
        {
            Status.Turn = Status.Turn == PlayerType.PLAYER_1 ? PlayerType.PLAYER_2 : PlayerType.PLAYER_1;
        }        

        public Warrior createWarrior(char warriorType)
        {
            Warrior warrior = null;

            if (char.ToUpper(warriorType) == 'M')
                warrior = new MeleeWarrior();
            else if (char.ToUpper(warriorType) == 'G')
                warrior = new MagicWarrior();

            return warrior;
        }

        public AttackRange WarriorAttackRange(Warrior warrior)
        {
            Board board = Board.boardInstance();
            int meleeWarriorsCount = board.countBoundaryElements(warrior, 1);
            int magicWarriorsCount = board.countBoundaryElements(warrior, 2);

            if (meleeWarriorsCount > magicWarriorsCount)
                return AttackRange.MeleeRange;
            else if (meleeWarriorsCount < magicWarriorsCount)
                return AttackRange.MagicRange;
            else // When both counts are equal
            {
                int chance = Util.random.Next(0, 2);
                if (chance == 0)
                    return AttackRange.MeleeRange;
                else
                    return AttackRange.MagicRange;
            }

        }

        public AttackType WarriorAttackType(Warrior warrior)
        {
            AttackType attackType = AttackType.NoAttack;
            AttackRange attackRange = WarriorAttackRange(warrior);
            int chance = Util.random.Next(1, 11);

            if (warrior is MeleeWarrior)
            {
                if (attackRange == AttackRange.MeleeRange)
                {
                    if (chance >= 1 && chance <= 8)
                        attackType = AttackType.MeleeAttack;
                    else if (chance == 9)
                        attackType = AttackType.MagicAttack;

                }
                else if (attackRange == AttackRange.MagicRange)
                {
                    if (chance >= 1 && chance <= 6)
                        attackType = AttackType.MagicAttack;
                }
            }
            else if (warrior is MagicWarrior)
            {
                if (attackRange == AttackRange.MagicRange)
                {
                    if (chance >= 1 && chance <= 9)
                        attackType = AttackType.MagicAttack;

                }
                else if (attackRange == AttackRange.MeleeRange)
                {
                    if (chance >= 1 && chance <= 8)
                        attackType = AttackType.MagicAttack;
                    else if (chance == 9)
                        attackType = AttackType.MeleeAttack;
                }
            }

            return attackType;
        }
    }
}
