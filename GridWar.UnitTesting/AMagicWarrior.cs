using GridWarModel.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridWar.UnitTesting
{
    [TestFixture]
    public class AMagicWarrior
    {
        [Test]
        public void ShouldSetMeleePowerBetweenOneAndThreeWhenConstructed()
        {
            var sut = new MagicWarrior();
            Assert.That(sut.MeleePower, Is.GreaterThanOrEqualTo(1));
            Assert.That(sut.MeleePower, Is.LessThanOrEqualTo(3));
        }

        [Test]
        public void ShouldSetMagicPowerBetweenThreeAndEightWhenConstructed()
        {
            var sut = new MagicWarrior();
            Assert.That(sut.MagicPower, Is.GreaterThanOrEqualTo(3));
            Assert.That(sut.MagicPower, Is.LessThanOrEqualTo(8));
        }
    }
}
