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
    public class ASurrenderAction
    {
        [Test]
        public void ShouldSetExitStatusToTrueWhenActionIsDone()
        {
            var warrior = new MeleeWarrior();
            var gameUtil = new GameUtil(null);
            var sut = new SurrenderAction(warrior, gameUtil);
            var exitStatusPre = Status.Exit;

            sut.doAction();
            Assert.That(Status.Exit, Is.Not.EqualTo(exitStatusPre));
        }
    }
}
