using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWarModel.Logic
{
    /// <summary>
    /// Board class is defined as singleton, because there should be only one Board in the game
    /// and thus single set of ROOMS. However, any occupied ROOMS have to be reset during testing
    /// </summary>
    public class Board
    {
        public const int BOARD_SIZE = 6;
        public int[,] ROOMS = new int[BOARD_SIZE, BOARD_SIZE];

        private static Board _board;
        private Board()
        {
            
        }

        public static Board boardInstance()
        {
            if (_board == null)
            {
                _board = new Board();
            }
            return _board;
        }
        
        public void MoveWarrior(Warrior warrior, Direction direction)
        {
            if (isInsideBoundary(warrior, direction))
            {
                MoveWarriorTo(warrior, direction);        
            }            
        }

        public bool isPositionOccupied(Position position)
        {
            return ROOMS[position.X, position.Y] == 1;
        }

        public bool isPositionOccupied(int x, int y)
        {
            return ROOMS[x, y] == 1;
        }     

        public int countBoundaryElements(Warrior warrior, int deltaFromWarrior)
        {
            int count = 0;

            int startX = warrior.Position.X - deltaFromWarrior;
            int startY = warrior.Position.Y - deltaFromWarrior;
            int endX = warrior.Position.X + deltaFromWarrior;
            int endY = warrior.Position.Y + deltaFromWarrior;

            for (int i = startX; i <= endX; i++)
            {
                if (i >= 0 && i < BOARD_SIZE)
                {
                    if (startY >= 0 && isPositionOccupied(i, startY)) count++;
                    if (endY < BOARD_SIZE && isPositionOccupied(i, endY)) count++;
                }
            }

            for (int i = startY + 1; i < endY; i++)
            {
                if (i >= 0 && i < BOARD_SIZE)
                {
                    if (startX >= 0 && isPositionOccupied(startX, i)) count++;
                    if (endX < BOARD_SIZE && isPositionOccupied(endX, i)) count++;
                }
            }

            return count;
        }        

        private bool isInsideBoundary(Warrior warrior, Direction direction)
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
        
        private void MoveWarriorTo(Warrior warrior, Direction direction)
        {            
            // New delta to move based on the direction
            int deltaX = 0;
            int deltaY = 0;
                     
            if (direction == Direction.EAST)
                deltaY += 1;
            else if (direction == Direction.WEST)
                deltaY -= 1;
            else if (direction == Direction.NORTH)
                deltaX -= 1;
            else if (direction == Direction.SOUTH)
                deltaX += 1;
            else if (direction == Direction.EAST_NORTH)
            {
                deltaY += 1;
                deltaX -= 1;
            }
            else if (direction == Direction.WEST_NORTH)
            {
                deltaY -= 1;
                deltaX -= 1;
            }
            else if (direction == Direction.WEST_SOUTH)
            {
                deltaY -= 1;
                deltaX += 1;
            }
            else if (direction == Direction.EAST_SOUTH)
            {
                deltaY += 1;
                deltaX += 1;
            }
            
            // Update new position                        
            if (!isNewPositionOccupied(warrior, deltaX, deltaY))
            {
                ROOMS[warrior.Position.X, warrior.Position.Y] = 0;             
                warrior.Position.X += deltaX;
                warrior.Position.Y += deltaY;
                ROOMS[warrior.Position.X, warrior.Position.Y] = 1;
            }
        }
        
        private bool isNewPositionOccupied(Warrior warrior, int deltaX, int deltaY)
        {
            return ROOMS[warrior.Position.X + deltaX, warrior.Position.Y + deltaY] == 1;
        }       
    }
}
