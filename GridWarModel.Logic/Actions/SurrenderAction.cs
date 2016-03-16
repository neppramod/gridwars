using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{    
    public class SurrenderAction : IPlayAction
    {
        Warrior warrior;

        public SurrenderAction(Warrior warrior)
        {
            this.warrior = warrior;
        }

        public void doAction(Game game)
        {
            Console.WriteLine("You lose! Better luck next time.");
            game.Exit = true;
        }

        public bool isDone()
        {
            return true;
        }
    }
}
