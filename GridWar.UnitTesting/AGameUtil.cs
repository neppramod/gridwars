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
    public class AGameUtil
    {
        [Test]
        public void ShouldBeAbleToCreateAWarrior()
        {
            // M for Melee, G for Magic
            var sut = new GameUtil().createWarrior('M');
            Assert.That(sut.GetType(), Is.EqualTo(new MeleeWarrior().GetType()));

            sut = new GameUtil().createWarrior('G');
            Assert.That(sut.GetType(), Is.EqualTo(new MagicWarrior().GetType()));

            // Should not create a warrior for any other character
            sut = new GameUtil().createWarrior('A');
            Assert.That(sut, Is.Null);
        }

        [Test]
        public void ShouldBeAbleToAddAWarrior()
        {
            var sut = new GameUtil();
            var warriorCountPre = sut.getWarriorsCountForAPlayer(Status.Turn);
            sut.addWarrior(sut.createWarrior('M'));
            Assert.That(sut.getWarriorsCountForAPlayer(Status.Turn), Is.EqualTo(warriorCountPre + 1));
        }

        [Test]
        public void ShouldBeAbleToDeleteAWarrior()
        {
            var sut = new GameUtil();
            var warrior = sut.createWarrior('m');
            sut.addWarrior(warrior);
            var warriroCountPre = sut.getWarriorsCountForAPlayer(Status.Turn);
            sut.deleteWarrior(warrior);

            Assert.That(sut.getWarriorsCountForAPlayer(Status.Turn), Is.EqualTo(warriroCountPre - 1));
        }

        [Test]
        public void ShouldBeAbleToSwitchTurns()
        {
            var sut = new GameUtil();
            PlayerType turnPre = Status.Turn;
            sut.switchTurn();

            Assert.That(Status.Turn, Is.Not.EqualTo(turnPre));
            Assert.That(Status.Turn.GetType(), Is.EqualTo(turnPre.GetType()));
        }

        [Test]
        public void ShouldSelectRangeBasedOnOccupiedBoundaryCells()
        {
            var sut = new GameUtil();
            var board = Board.boardInstance();
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
    }
}
