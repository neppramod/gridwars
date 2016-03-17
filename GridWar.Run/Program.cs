using GridWarModel.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWar.Run
{
    public class Program
    {
        public static void Main()
        {            
            Game game = new Game();
            game.welcomeScreen();            
            Console.ReadKey();
        }
    }
}

