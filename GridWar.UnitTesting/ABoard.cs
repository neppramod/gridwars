using GridWarModel.Logic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWar.UnitTesting
{
    [TestFixture]    
    public class ABoard
    {
        [Test]
        public void ShouldOnlyBeAbleToCreateOneBoard()
        {
            Board board1 = Board.boardInstance();
            Board board2 = Board.boardInstance();
            Assert.That(board1, Is.EqualTo(board2));
        }    

        [Test]
        public void ShouldDetectIfAPositionIsOccupied()
        {
            var sut = Board.boardInstance();

            Assert.That(sut.IsPositionOccupied(2, 4), Is.EqualTo(false));
            sut.ROOMS[2, 4] = 1;
            Assert.That(sut.IsPositionOccupied(2, 4), Is.EqualTo(true));
            Assert.That(sut.IsPositionOccupied(new Position { X = 2, Y = 4 }), Is.EqualTo(true));
        }

        [Test]
        public void ShouldCountNumberOfMeleeRangeOccupiedBoundaryRooms()
        {
            var sut = Board.boardInstance();            
            var position = new Position { X = 3, Y = 3 };

            sut.ROOMS[2, 4] = 1;
            sut.ROOMS[4, 3] = 1;

            Assert.That(sut.CountBoundaryElements(position, 1), Is.EqualTo(2));
        }

        [Test]
        public void ShouldCountNumberOfMagicRangeOccupiedBoundaryRooms()
        {
            var sut = Board.boardInstance();            
            var position = new Position { X = 3, Y = 3 };

            sut.ROOMS[1,1] = 1;
            sut.ROOMS[1,4] = 1;
            sut.ROOMS[1,5] = 1;
            sut.ROOMS[5,1] = 1;
            sut.ROOMS[5,4] = 1;

            Assert.That(sut.CountBoundaryElements(position, 2), Is.EqualTo(5));
        }

        [Test]
        public void ShouldCountNumberOfMeleeRangeOccupiedBoundaryRoomsNearEdges()
        {
            var sut = Board.boardInstance();            
            var position = new Position { X = 3, Y = 5 };

            sut.ROOMS[2, 4] = 1;
            sut.ROOMS[4, 4] = 1;
            sut.ROOMS[4, 5] = 1;

            Assert.That(sut.CountBoundaryElements(position, 1), Is.EqualTo(3));
        }

        [Test]
        public void ShouldCountNumberOfMagicRangeOccupiedBoundaryRoomsNearEdges()
        {
            var sut = Board.boardInstance();            
            var position = new Position { X = 0, Y = 0 };

            sut.ROOMS[1,2] = 1;
            sut.ROOMS[2,0] = 1;
            sut.ROOMS[2,2] = 1;

            Assert.That(sut.CountBoundaryElements(position, 2), Is.EqualTo(3));
        }

        [Test]
        public void ShouldDetectIfPositionOutsideAdjacentPositionTowardsADirectionIsInsideBoardBoundary()
        {
            var sut = Board.boardInstance();
            var currentPositon = new Position { X = 5, Y = 5 };
            Assert.That(sut.IsPositionInsideBoundary(currentPositon, Direction.EAST_SOUTH, Direction.SOUTH), Is.EqualTo(false));
            Assert.That(sut.IsPositionInsideBoundary(currentPositon, Direction.WEST_NORTH, Direction.WEST), Is.EqualTo(true));
        }

        [Test]
        public void ShouldGetAPositionInADirectionInTheBoundaryPosition()
        {
            var sut = Board.boardInstance();
            var currentPosition = new Position { X = 2, Y = 2 };
            var newPosition = sut.GetPositionInADirection(currentPosition, Direction.EAST);
            Assert.That(newPosition, Is.EqualTo(new Position { X = currentPosition.X, Y = currentPosition.Y + 1 }));
        }

        [Test]
        public void ShouldGetAPositionInADirectionOutsideTheBoundaryPosition()
        {
            var sut = Board.boardInstance();
            var currentPosition = new Position { X = 2, Y = 2 };
            var newPosition = sut.GetPositionInADirection(currentPosition, Direction.EAST, Direction.EAST_NORTH);
            Assert.That(newPosition, Is.EqualTo(new Position { X = currentPosition.X - 1, Y = currentPosition.Y + 2}));
        }

       [Test]
       public void ShouldDetectIfAdjacentPositionTowardsADirectionIsInsideBoardBoundary()
        {
            var sut = Board.boardInstance();
            var currentPositon = new Position { X = 5, Y = 5 };
            Assert.That(sut.IsPositionInsideBoundary(currentPositon, Direction.EAST_SOUTH), Is.EqualTo(false));
            Assert.That(sut.IsPositionInsideBoundary(currentPositon, Direction.WEST_NORTH), Is.EqualTo(true));
        }
        
        [Test]
        public void ShouldBeAbleToFindMovementDeltaToADirection()
        {
            var sut = Board.boardInstance();
            int preDeltaX = 0;
            int preDeltaY = 0;

            Position deltaPosition = sut.GetMovementDelta(Direction.EAST_NORTH);
            Assert.That(deltaPosition.X, Is.EqualTo(preDeltaX - 1));
            Assert.That(deltaPosition.Y, Is.EqualTo(preDeltaY + 1));
        } 
        
        [Test]
        public void ShouldDetectOccupiedNewPositionInDeltaDistanceAway()
        {
            var sut = Board.boardInstance();
            var currentPosition = new Position { X = 2, Y = 2 };
                        
            Assert.That(sut.IsNewPositionOccupied(currentPosition, 0, 2), Is.EqualTo(false));
            sut.ROOMS[2, 4] = 1;
            Assert.That(sut.IsNewPositionOccupied(currentPosition, 0, 2), Is.EqualTo(true));
        }       
        
        [TearDown]
        public void Reset()
        {
            // Reset the used ROOM
            for (int i = 0; i < Board.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                {
                    Board.boardInstance().ROOMS[i,j] = 0;
                }
            }
            
        }
        
    }
}
