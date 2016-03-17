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
        Game game;

        public AttackAction(Warrior warrior)
        {
            this.warrior = warrior;
        }

        public void doAction(Game game)
        {
            this.game = game;
            if (game.ActionCount == 0 || game.ActionCount == 1)
                game.ActionCount++;

            Direction direction = Board.chooseADirection();
            if (direction != Direction.INVALID_DIRECTION)
            {                
                 attack(direction);                
            } 
        }

        private void attack(Direction direction)
        {
            Console.WriteLine("\nWarrior " + this.warrior.Id + " is attacking in " + direction.ToString());
        }

        public bool isDone()
        {
            if (game.ActionCount == 2)
            {
                game.ActionCount = 0; //reset count
                return true;
            } else
            {
                return false;
            }
        }
    }
}
