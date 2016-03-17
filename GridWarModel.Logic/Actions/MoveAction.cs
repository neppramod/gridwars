using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    public class MoveAction : IPlayAction
    {
        Warrior warrior;        

        public MoveAction(Warrior warrior)
        {
            this.warrior = warrior;
        }

        public void doAction()
        {            
            if (Status.ActionCount == 0 || Status.ActionCount == 1)
                Status.ActionCount++;
            
            if (Status.ActionDirection != Direction.INVALID_DIRECTION)
            {
                try
                {
                    this.warrior.Move(Status.ActionDirection);
                } catch(InvalidOperationException)
                {
                    Console.WriteLine("\nCould not move the player to " + Status.ActionDirection.ToString() + ", you miss a chance");
                }
            }                     
        }

        public bool isDone()
        {
            if (Status.ActionCount == 2)
            {
                Status.ActionCount = 0; // reset count
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
