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

        public static List<Warrior> getWarriorsForAPlayer(PlayerType playerType)
        {
            return allWarriors.FindAll(w => w.Player == playerType);
        }

        public static void switchTurn()
        {
            Status.Turn = Status.Turn == PlayerType.PLAYER_1 ? PlayerType.PLAYER_2 : PlayerType.PLAYER_1;
        }

        public static bool isPositionOccupied(Position position)
        {
            return Board.ROOMS[position.X, position.Y] == 1;
        }

        public static Warrior createWarrior(char warriorType)
        {
            Warrior warrior;

            if (char.ToUpper(warriorType) == 'M')
                warrior = new MeleeWarrior();
            else
                warrior = new MagicWarrior();

            return warrior;
        }
    }
}
