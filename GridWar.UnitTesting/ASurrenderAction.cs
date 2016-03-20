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
            var mockItem = new Mock<Warrior>();
            var mockGameUtil = new Mock<GameUtil>();
            var sut = new SurrenderAction(mockItem.Object, mockGameUtil.Object);
            var exitStatusPre = Status.Exit;

            sut.doAction();
            Assert.That(Status.Exit, Is.Not.EqualTo(exitStatusPre));
        }
    }
}
