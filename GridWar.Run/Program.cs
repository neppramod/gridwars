using GridWarModel.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWar.Run
{
    public class Execute
    {
        public static void Main()
        {
            int selectPlayer = Util.random.Next(0, 2);
            Game game = new Game();
            game.welcomeScreen();            
            Console.ReadKey();
        }
    }
}

