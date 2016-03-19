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
    }
}
