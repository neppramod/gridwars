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
        Game game;

        public MoveAction(Warrior warrior)
        {
            this.warrior = warrior;
        }

        public void doAction(Game game)
        {
            this.game = game;
            if (game.ActionCount == 0)
                game.ActionCount++;

            Direction direction = chooseADirection();
            if (direction != Direction.INVALID_DIRECTION)
            {
                try
                {
                    this.warrior.Move(direction);
                } catch(InvalidOperationException invalidOpearationException)
                {
                    Console.WriteLine("\nCould not move the player to " + direction.ToString() + ", you miss a chance");
                }
            }                     
        }

        private Direction chooseADirection()
        {
            Console.WriteLine("\nMove direction: E:EAST, W:WEST, N:NORTH, S:SOUTH, A:EAST_NORTH, B:EAST_SOUTH, C:WEST_SOUTH, D:WEST_NORTH");
            char inputChar = char.ToUpper(Console.ReadKey().KeyChar);

            if (inputChar == 'E')
                return Direction.EAST;
            else if (inputChar == 'W')
                return Direction.WEST;
            else if (inputChar == 'N')
                return Direction.NORTH;
            else if (inputChar == 'S')
                return Direction.SOUTH;
            else if (inputChar == 'A')
                return Direction.EAST_NORTH;
            else if (inputChar == 'B')
                return Direction.EAST_SOUTH;
            else if (inputChar == 'C')
                return Direction.WEST_SOUTH;
            else if (inputChar == 'D')
                return Direction.WEST_NORTH;
            else
            {
                Console.WriteLine("Did not select a proper direction. You miss.");
                return Direction.INVALID_DIRECTION;
            }
        }

        public bool isDone()
        {
            if ( game.ActionCount == 2)
            {
                game.ActionCount = 0; // reset count
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
