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
        public void ShouldBeAbleToMoveAWarrior()
        {
            var sut = Board.boardInstance();

            // Create a Fake
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 2, Y = 2 };            

            var positionXpre = warrior.Position.X;
            var positionYpre = warrior.Position.Y;

            sut.MoveWarrior(warrior, Direction.EAST_NORTH);

            Assert.That(warrior.Position.X, Is.EqualTo(positionXpre -1));
            Assert.That(warrior.Position.Y, Is.EqualTo(positionYpre + 1));
        }
        
        
        [Test]
        public void APositionShouldBeOccupiedAfterAWarriorIsMovedThere()
        {
            var sut = Board.boardInstance();
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 2, Y = 2 };

            var positionXpre = warrior.Position.X;
            var positionYpre = warrior.Position.Y;
                        
            sut.MoveWarrior(warrior, Direction.EAST_NORTH);

            Assert.That(sut.isPositionOccupied(new Position { X = positionXpre - 1, Y = positionYpre + 1 }), Is.EqualTo(true));            
        }

        [Test]
        public void ShouldCountNumberOfMeleeRangeOccupiedBoundaryRooms()
        {
            var sut = Board.boardInstance();
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 3, Y = 3 };

            sut.ROOMS[2, 4] = 1;
            sut.ROOMS[4, 3] = 1;

            Assert.That(sut.countBoundaryElements(warrior, 1), Is.EqualTo(2));
        }

        [Test]
        public void ShouldCountNumberOfMagicRangeOccupiedBoundaryRooms()
        {
            var sut = Board.boardInstance();
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 3, Y = 3 };

            sut.ROOMS[1,1] = 1;
            sut.ROOMS[1,4] = 1;
            sut.ROOMS[1,5] = 1;
            sut.ROOMS[5,1] = 1;
            sut.ROOMS[5,4] = 1;

            Assert.That(sut.countBoundaryElements(warrior, 2), Is.EqualTo(5));
        }

        [Test]
        public void ShouldCountNumberOfMeleeRangeOccupiedBoundaryRoomsNearEdges()
        {
            var sut = Board.boardInstance();
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 3, Y = 5 };

            sut.ROOMS[2, 4] = 1;
            sut.ROOMS[4, 4] = 1;
            sut.ROOMS[4, 5] = 1;

            Assert.That(sut.countBoundaryElements(warrior, 1), Is.EqualTo(3));
        }

        [Test]
        public void ShouldCountNumberOfMagicRangeOccupiedBoundaryRoomsNearEdges()
        {
            var sut = Board.boardInstance();
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 0, Y = 0 };

            sut.ROOMS[1,2] = 1;
            sut.ROOMS[2,0] = 1;
            sut.ROOMS[2,2] = 1;

            Assert.That(sut.countBoundaryElements(warrior, 2), Is.EqualTo(3));
        }

        [Test]
        public void ShouldSelectRangeBasedOnOccupiedBoundaryCells()
        {
            var sut = Board.boardInstance();
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 0, Y = 0 };

            sut.ROOMS[0,1] = 1;
            sut.ROOMS[1,1] = 1;
            sut.ROOMS[1,2] = 1;
            sut.ROOMS[2,0] = 1;
            sut.ROOMS[2,2] = 1;

            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MagicRange));

            warrior.Position = new Position { X = 0, Y = 3 };
            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MagicRange));

            warrior.Position = new Position { X = 2, Y = 1 };
            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MeleeRange));

            sut.ROOMS[0,2] = 1;
            sut.ROOMS[2,3] = 1;
            sut.ROOMS[4,0] = 1;
            sut.ROOMS[4,2] = 1;
            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MagicRange));
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
