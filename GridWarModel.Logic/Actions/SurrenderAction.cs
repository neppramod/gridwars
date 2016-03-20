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
        GameUtil gameUtil;

        public SurrenderAction(Warrior warrior, GameUtil gameUtil)
        {
            this.warrior = warrior;
            this.gameUtil = gameUtil;
        }

        public void doAction()
        {            
            Status.Exit = true;
        }

        public bool isDone()
        {
            return true;
        }
    }
}
