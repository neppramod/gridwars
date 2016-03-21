using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class AttackAction : IPlayAction
    {
        Warrior warrior;
        GameUtil gameUtil;

        public AttackAction(Warrior warrior, GameUtil gameUtil)
        {
            this.warrior = warrior;
            this.gameUtil = gameUtil;
        }

        public void doAction()
        {            
            if (Status.ActionCount == 0 || Status.ActionCount == 1)
                Status.ActionCount++;
            
            if (Status.ActionDirection != Direction.INVALID_DIRECTION)
            {                
                 attack();                
            } 
        }

        private void attack()
        {
            // Attack Action
        }

        public bool isDone()
        {
            if (Status.ActionCount == 2)
            {
                Status.ActionCount = 0; //reset count
                return true;
            } else
            {
                return false;
            }
        }
    }
}
