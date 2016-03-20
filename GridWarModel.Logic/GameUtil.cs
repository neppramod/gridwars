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
        public List<Warrior> allWarriors = new List<Warrior>();

        public List<Warrior> getWarriorsForAPlayer(PlayerType playerType)
        {
            return allWarriors.FindAll(w => w.Player == playerType);
        }

        public int getWarriorsCountForAPlayer(PlayerType playerType)
        {
            return getWarriorsForAPlayer(playerType).Count;
        }

        /// <summary>
        /// If a warrior exist find a warrior at ROOM[X,Y]
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>

        public Warrior findWarriorAtARoom(int X, int Y)
        {
            Warrior warrior = null;
            Position findPosition = new Position { X = X, Y = Y };

            warrior = allWarriors.FirstOrDefault(w => w.Position.Equals(findPosition));

            return warrior;
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
            int meleeWarriorsCount = board.CountBoundaryElements(warrior.Position, 1);
            int magicWarriorsCount = board.CountBoundaryElements(warrior.Position, 2);

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

        /// <summary>
        /// Attack a boundary cell. In case of MeleeAttack AttackType firstDirection is used, and for  
        /// MagicAttack secondDirection can also be used (if warrior attacks outside the boundary).
        /// </summary>
        /// <param name="warrior">Attacking warrior</param>
        /// <param name="attackType">MeleeAttack or MagicAttack type provided</param>
        /// <param name="firstDirection">MeleeAttack can only attack in boundaries determined by firstDirection. MagicAttack can also attack with firstDirection</param>
        /// <param name="secondDirection">MagicAttack can use secondDirection to use cells right outside first boundary</param>
        /// <returns>Whether a warrior could attack at a location or not ?</returns>        
        public bool WarriorAttack(Warrior warrior, AttackType attackType, Direction firstDirection, Direction secondDirection = Direction.INVALID_DIRECTION)
        {
            Position warriorPosition = warrior.Position;
            Board board = Board.boardInstance();

            if (board.IsPositionInsideBoundary(warriorPosition, firstDirection, secondDirection))
            {
                Position attackPosition = board.GetPositionInADirection(warriorPosition, firstDirection, secondDirection);
                Warrior opponent = findWarriorAtARoom(attackPosition.X, attackPosition.Y);

                if (attackType == AttackType.MeleeAttack)
                {
                    // Make sure for a MeleeAttack boundary room was selected
                    if (secondDirection != Direction.INVALID_DIRECTION)
                        return false;

                    double meleeDamage = opponent.MeleePower - (opponent.DefensePercentage * opponent.MeleePower);
                    opponent.HitPoints -= meleeDamage;

                    return true;
                } else if (attackType == AttackType.MagicAttack)
                {
                    double magicDamage = opponent.MagicPower - (opponent.DefensePercentage * opponent.MagicPower);
                    opponent.HitPoints -= magicDamage;

                    return true;
                }                

                // After attack power of attacking warrior
                if (warrior is MeleeWarrior)
                {
                    double meleePower = warrior.MeleePower + 0.5;
                    warrior.MeleePower = meleePower <= Warrior.MAX_MELEE_POWER ? meleePower : Warrior.MAX_MELEE_POWER;
                    double magicPower = warrior.MagicPower + 0.125;
                    warrior.MagicPower = magicPower <= Warrior.MAX_MAGIC_POWER ? magicPower : Warrior.MAX_MAGIC_POWER;
                } else if (warrior is MagicWarrior)
                {
                    double meleePower = warrior.MeleePower + 0.125;
                    warrior.MeleePower = meleePower <= Warrior.MAX_MELEE_POWER ? meleePower : Warrior.MAX_MELEE_POWER;
                    double magicPower = warrior.MagicPower + 0.5;
                    warrior.MagicPower = magicPower <= Warrior.MAX_MAGIC_POWER ? magicPower : Warrior.MAX_MAGIC_POWER;
                }

                // After being attacked opponents power change
                double defencePercentage = opponent.DefensePercentage + 0.25;
                opponent.DefensePercentage = defencePercentage <= Warrior.MAX_DEFENSE_PERCENTAGE ? defencePercentage : Warrior.MAX_DEFENSE_PERCENTAGE;

                // Opponent vanquishing
                if (opponent.HitPoints <= 0)
                {
                    double defensePercentage = warrior.DefensePercentage + 1;
                    warrior.DefensePercentage = defensePercentage <= Warrior.MAX_DEFENSE_PERCENTAGE ? defensePercentage : Warrior.MAX_DEFENSE_PERCENTAGE;
                    if (warrior is MeleeWarrior)
                    {
                        double meleePower = warrior.MeleePower + 1;
                        warrior.MeleePower = meleePower <= Warrior.MAX_MELEE_POWER ? meleePower : Warrior.MAX_MELEE_POWER;
                        double magicPower = warrior.MagicPower + 0.25;
                        warrior.MagicPower = magicPower <= Warrior.MAX_MAGIC_POWER ? magicPower : Warrior.MAX_MAGIC_POWER;
                    }
                    else if (warrior is MagicWarrior)
                    {
                        double meleePower = warrior.MeleePower + 0.25;
                        warrior.MeleePower = meleePower <= Warrior.MAX_MELEE_POWER ? meleePower : Warrior.MAX_MELEE_POWER;
                        double magicPower = warrior.MagicPower + 1;
                        warrior.MagicPower = magicPower <= Warrior.MAX_MAGIC_POWER ? magicPower : Warrior.MAX_MAGIC_POWER;
                    }

                    // remove the warrior from the list
                    deleteWarrior(warrior);
                }
            }          

            return false;   
        }

        public void MoveWarrior(Warrior warrior, Direction direction)
        {
            if (isWarriorInsideBoundary(warrior, direction))
            {
                Board board = Board.boardInstance();
                Position deltaMovement = board.GetMovementDelta(direction);

                int deltaX = deltaMovement.X;
                int deltaY = deltaMovement.Y;

                // Update new position                        
                if (!board.IsNewPositionOccupied(warrior.Position, deltaX, deltaY))
                {
                    board.ROOMS[warrior.Position.X, warrior.Position.Y] = 0;
                    warrior.Position.X += deltaX;
                    warrior.Position.Y += deltaY;
                    board.ROOMS[warrior.Position.X, warrior.Position.Y] = 1;

                    // After a warrior moves defence power increases
                    double defencePercentage = warrior.DefensePercentage + 0.125;
                    warrior.DefensePercentage = defencePercentage <= Warrior.MAX_DEFENSE_PERCENTAGE ? defencePercentage : Warrior.MAX_DEFENSE_PERCENTAGE;
                }
            }
        }

        private bool isWarriorInsideBoundary(Warrior warrior, Direction direction)
        {                        
            return Board.boardInstance().IsPositionInsideBoundary(warrior.Position, direction);
        }
    }
}
