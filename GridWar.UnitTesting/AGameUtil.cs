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
    }
}
