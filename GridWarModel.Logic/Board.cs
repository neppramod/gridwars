using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{

    public class Board
    {
        public const int BOARD_SIZE = 6;
        public static int[,] ROOMS = new int[BOARD_SIZE, BOARD_SIZE];
        
        public static void Move(Warrior warrior, Direction direction)
        {
            if (CanMoveWarrierTo(warrior, direction))
                MoveWarriorTo(warrior, direction);
            else throw new InvalidOperationException("\nCan't move warrier to " + direction.ToString());            
        }

              
        private static bool CanMoveWarrierTo(Warrior warrior, Direction direction)
        {
            if (warrior.Position.Y >= BOARD_SIZE - 1 && (direction == Direction.EAST || direction == Direction.EAST_NORTH || direction == Direction.EAST_SOUTH)) // Can't go right off the edge
                return false;
            else if (warrior.Position.Y <= 0 && (direction == Direction.WEST || direction == Direction.WEST_NORTH || direction == Direction.WEST_SOUTH)) // Can't go left off the edge
                return false;
            if (warrior.Position.X >= BOARD_SIZE - 1 && (direction == Direction.SOUTH || direction == Direction.EAST_SOUTH || direction == Direction.WEST_SOUTH)) // Can't go down off the edge
                return false;
            else if (warrior.Position.X <= 0 && (direction == Direction.NORTH || direction == Direction.EAST_NORTH || direction == Direction.WEST_NORTH)) // Can't go up off the edge
                return false;            

            return true;
        }
        
        private static void MoveWarriorTo(Warrior warrior, Direction direction)
        {
            // Previous position
            int preWarriorX = warrior.Position.X;
            int preWarriorY = warrior.Position.Y;
                     
            if (direction == Direction.EAST)
                warrior.Position.Y += 1;
            else if (direction == Direction.WEST)
                warrior.Position.Y -= 1;
            else if (direction == Direction.NORTH)
                warrior.Position.X -= 1;
            else if (direction == Direction.SOUTH)
                warrior.Position.X += 1;
            else if (direction == Direction.EAST_NORTH)
            {
                warrior.Position.Y += 1;
                warrior.Position.X -= 1;
            }
            else if (direction == Direction.WEST_NORTH)
            {
                warrior.Position.Y -= 1;
                warrior.Position.X -= 1;
            }
            else if (direction == Direction.WEST_SOUTH)
            {
                warrior.Position.Y -= 1;
                warrior.Position.X += 1;
            }
            else if (direction == Direction.EAST_SOUTH)
            {
                warrior.Position.Y += 1;
                warrior.Position.X += 1;
            }

            // Clear previous position. Set new position
            if (ROOMS[warrior.Position.X, warrior.Position.Y] == 1)
            {
                Console.WriteLine("\nCannot move to " + warrior.Position.X + ", " + warrior.Position.Y + ". A player exists.");
                warrior.Position.X = preWarriorX;
                warrior.Position.Y = preWarriorY;
            } else
            {
                ROOMS[preWarriorX, preWarriorY] = 0;
                ROOMS[warrior.Position.X, warrior.Position.Y] = 1;
                Console.WriteLine("\nPlayer moved from " + preWarriorX + "," + preWarriorY + " to " + warrior.Position.X + ", " + warrior.Position.Y);
            }
        }

        public static Direction chooseADirection()
        {
            Console.WriteLine("\nDirection: E:EAST, W:WEST, N:NORTH, S:SOUTH, A:EAST_NORTH, B:EAST_SOUTH, C:WEST_SOUTH, D:WEST_NORTH");
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

        public static void PrintBoard()
        {
            Console.WriteLine("\nBoard\n");
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    Console.Write(" | " + ROOMS[i,j] + " | ");
                }
                Console.WriteLine();
            }
        }
    }
}
