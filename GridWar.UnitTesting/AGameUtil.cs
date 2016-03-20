﻿using GridWarModel.Logic;
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
    public class AGameUtil
    {
        [Test]
        public void ShouldBeAbleToCreateAWarrior()
        {
            // M for Melee, G for Magic
            var gameUtil = new GameUtil(null);
            var sut = gameUtil.createWarrior('M');
            Assert.That(sut.GetType(), Is.EqualTo(new MeleeWarrior().GetType()));

            sut = gameUtil.createWarrior('G');
            Assert.That(sut.GetType(), Is.EqualTo(new MagicWarrior().GetType()));

            // Should not create a warrior for any other character
            sut = gameUtil.createWarrior('A');
            Assert.That(sut, Is.Null);
        }

        [Test]
        public void ShouldBeAbleToAddAWarrior()
        {
            var sut = new GameUtil(null);
            var warriorCountPre = sut.getWarriorsCountForAPlayer(Status.Turn);
            sut.addWarrior(sut.createWarrior('M'));
            Assert.That(sut.getWarriorsCountForAPlayer(Status.Turn), Is.EqualTo(warriorCountPre + 1));
        }

        [Test]
        public void ShouldBeAbleToDeleteAWarrior()
        {
            var sut = new GameUtil(null);
            var warrior = sut.createWarrior('m');
            sut.addWarrior(warrior);
            var warriorCountPre = sut.getWarriorsCountForAPlayer(Status.Turn);
            sut.deleteWarrior(warrior);

            Assert.That(sut.getWarriorsCountForAPlayer(Status.Turn), Is.EqualTo(warriorCountPre - 1));
        }

        [Test]
        public void ShouldBeAbleToSwitchTurns()
        {
            var sut = new GameUtil(null);
            PlayerType turnPre = Status.Turn;
            sut.switchTurn();

            Assert.That(Status.Turn, Is.Not.EqualTo(turnPre));
            Assert.That(Status.Turn.GetType(), Is.EqualTo(turnPre.GetType()));
        }

        [Test]
        public void ShouldSelectRangeBasedOnOccupiedBoundaryCells()
        {
            var board = new Board();
            var sut = new GameUtil(board);            
            var warrior = new MeleeWarrior();
            warrior.Position = new Position { X = 0, Y = 0 };

            board.ROOMS[0, 1] = 1;
            board.ROOMS[1, 1] = 1;
            board.ROOMS[1, 2] = 1;
            board.ROOMS[2, 0] = 1;
            board.ROOMS[2, 2] = 1;

            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MagicRange));

            warrior.Position = new Position { X = 0, Y = 3 };
            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MagicRange));

            warrior.Position = new Position { X = 2, Y = 1 };
            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MeleeRange));

            board.ROOMS[0, 2] = 1;
            board.ROOMS[2, 3] = 1;
            board.ROOMS[4, 0] = 1;
            board.ROOMS[4, 2] = 1;
            Assert.That(sut.WarriorAttackRange(warrior), Is.EqualTo(AttackRange.MagicRange));
        }

        [Test]
        public void ShouldFindAWarriorAtAPosition()
        {
            var sut = new GameUtil(null);
            MeleeWarrior meleeWarrior = new MeleeWarrior();
            meleeWarrior.Position = new Position { X = 3, Y = 4 };
            sut.addWarrior(meleeWarrior);

            meleeWarrior = new MeleeWarrior();
            meleeWarrior.Position = new Position { X = 2, Y = 1 };
            sut.addWarrior(meleeWarrior);

            Warrior foundWarrior1 = sut.findWarriorAtARoom(3, 4);
            Assert.That(foundWarrior1, Is.Not.Null);

            Warrior foundWarrior2 = sut.findWarriorAtARoom(2, 2);
            Assert.That(foundWarrior2, Is.EqualTo(null));
        }
		
		[Test]
        public void ShouldBeAbleToMoveAWarrior()
        {
            Board board = new Board();
            var sut = new GameUtil(board);

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
            var sut = new Board();
            var warrior = new MeleeWarrior();
			var gameUtil = new GameUtil(sut);
			
            warrior.Position = new Position { X = 2, Y = 2 };

            var positionXpre = warrior.Position.X;
            var positionYpre = warrior.Position.Y;
                        
            gameUtil.MoveWarrior(warrior, Direction.EAST_NORTH);

            Assert.That(sut.IsPositionOccupied(new Position { X = positionXpre - 1, Y = positionYpre + 1 }), Is.EqualTo(true));            
        }
    }
}
