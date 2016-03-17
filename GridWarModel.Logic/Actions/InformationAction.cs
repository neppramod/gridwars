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

        public InformationAction(Warrior warrior)
        {
            this.warrior = warrior;
        }

        public void doAction(Game game)
        {
            Console.WriteLine("Statistics");
            Console.WriteLine("Selected warrior: " + warrior.Id);
            List<Warrior> warriors = game.getWarriorsForAPlayer(warrior.Player);
            var totalHitPoints = warriors.Sum(w => w.HitPoints);
            Console.WriteLine("Total hit points: " + totalHitPoints);
        }

        public bool isDone()
        {
            return false;
        }
    }
}
