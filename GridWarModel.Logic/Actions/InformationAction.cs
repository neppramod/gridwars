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
            // Show information
        }

        public bool isDone()
        {
            return false;
        }
    }
}
