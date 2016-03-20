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

        // Should be a valid location inside the board. Didn't check valid location here, because it is checked in 
        // IsPositionInsideBoundary method.
        // It does not make sense to return false for array index out of value.
        // Which means a user can move to that place (which is wrong).
        public bool IsPositionOccupied(Position position)
        {
            return ROOMS[position.X, position.Y] == 1;
        }

        public bool IsPositionOccupied(int x, int y)
        {
            return ROOMS[x, y] == 1;
        }     

        public int CountBoundaryElements(Position position, int deltaFromWarrior)
        {
            int count = 0;

            int startX = position.X - deltaFromWarrior;
            int startY = position.Y - deltaFromWarrior;
            int endX = position.X + deltaFromWarrior;
            int endY = position.Y + deltaFromWarrior;

            for (int i = startX; i <= endX; i++)
            {
                if (i >= 0 && i < BOARD_SIZE)
                {
                    if (startY >= 0 && IsPositionOccupied(i, startY)) count++;
                    if (endY < BOARD_SIZE && IsPositionOccupied(i, endY)) count++;
                }
            }

            for (int i = startY + 1; i < endY; i++)
            {
                if (i >= 0 && i < BOARD_SIZE)
                {
                    if (startX >= 0 && IsPositionOccupied(startX, i)) count++;
                    if (endX < BOARD_SIZE && IsPositionOccupied(endX, i)) count++;
                }
            }

            return count;
        }
        
        public bool IsPositionInsideBoundary(Position position, Direction firstDirection, Direction secondDirection = Direction.INVALID_DIRECTION)
        {
            if (secondDirection == Direction.INVALID_DIRECTION)
            {
                return IsPositionInsideBoundary(position, firstDirection);
            }
            else
            {   // Create a new position based on direction1 and try to find if directio2 is inside boundary from that position
                Position deltaPosition = GetMovementDelta(firstDirection);
                Position newPosition = new Position { X = position.X + deltaPosition.X, Y = position.Y + deltaPosition.Y };
                return IsPositionInsideBoundary(newPosition, secondDirection);
            }            
        }

        /// <summary>
        /// A way for the player to check if a position is attackable. 
        /// The secondDirection gives a way to get position coordinates two cells away
        /// Using secondDirection a warrior can attack to that cell
        /// E.g., To attack from positon = {2,2} to {4,3}, firstDirection = Direction.SOUTH or Direction.SOUTH_EAST, 
        /// secondDirection = Direction=SOUTH_EAST or Direction.SOUTH
        /// </summary>
        /// <param name="position">Current position</param>
        /// <param name="firstDirection">Adjacent position direction</param>
        /// <param name="secondDirection">If secondDirection == Direction.INAVALID_DIRECTION only firstDirection is used</param>
        /// <returns></returns>
        public Position GetPositionInADirection(Position position, Direction firstDirection, Direction secondDirection = Direction.INVALID_DIRECTION)
        {
            Position deltaPosition = GetMovementDelta(firstDirection);
            Position newPosition = new Position { X = position.X + deltaPosition.X, Y = position.Y + deltaPosition.Y };

            if (secondDirection != Direction.INVALID_DIRECTION)
            {
                Position newDelta = GetMovementDelta(secondDirection);
                newPosition.X += newDelta.X ;
                newPosition.Y += newDelta.Y;
            }

            return newPosition;
        }

        public bool IsPositionInsideBoundary(Position position, Direction direction)
        {
            if (position.Y >= BOARD_SIZE - 1 && (direction == Direction.EAST || direction == Direction.EAST_NORTH || direction == Direction.EAST_SOUTH)) // Can't go right off the edge
                return false;
            else if (position.Y <= 0 && (direction == Direction.WEST || direction == Direction.WEST_NORTH || direction == Direction.WEST_SOUTH)) // Can't go left off the edge
                return false;
            if (position.X >= BOARD_SIZE - 1 && (direction == Direction.SOUTH || direction == Direction.EAST_SOUTH || direction == Direction.WEST_SOUTH)) // Can't go down off the edge
                return false;
            else if (position.X <= 0 && (direction == Direction.NORTH || direction == Direction.EAST_NORTH || direction == Direction.WEST_NORTH)) // Can't go up off the edge
                return false;

            return true;
        }

        public Position GetMovementDelta(Direction direction)
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

            return new Position { X = deltaX, Y = deltaY };
        }

        // Should be a valid location inside the board. Didn't check valid location here, because it is checked in 
        // IsPositionInsideBoundary method.
        // It does not make sense to return false for array index out of value.
        // Which means a user can move to that place (which is wrong).
        public bool IsNewPositionOccupied(Position position, int deltaX, int deltaY)
        {            
            return ROOMS[position.X + deltaX, position.Y + deltaY] == 1;
        }       
    }
}
