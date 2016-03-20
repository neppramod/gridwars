using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class InformationAction : IPlayAction
    {
        Warrior warrior;
        GameUtil gameUtil;

        public InformationAction(Warrior warrior, GameUtil gameUtil)
        {
            this.warrior = warrior;
            this.gameUtil = gameUtil;
        }

        public void doAction()
        {
            Console.WriteLine("Statistics");
            Console.WriteLine("Selected warrior: " + warrior.Id);

            List<Warrior> warriors = this.gameUtil.getWarriorsForAPlayer(warrior.Player);
            var totalHitPoints = warriors.Sum(w => w.HitPoints);
            Console.WriteLine("Total hit points: " + totalHitPoints);
        }

        public bool isDone()
        {
            return false;
        }
    }
}
