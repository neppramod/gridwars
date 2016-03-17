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
    public class AMeleeWarrior
    {
        [Test]
        public void ShouldSetMeleePowerBetweenFiveAndTenWhenConstructed()
        {
            var sut = new MeleeWarrior();
            Assert.That(sut.MeleePower, Is.GreaterThanOrEqualTo(5));
            Assert.That(sut.MeleePower, Is.LessThanOrEqualTo(10));
        }

        [Test]
        public void ShouldSetMagicPowerBetweenOneAndThreeWhenConstructed()
        {
            var sut = new MeleeWarrior();
            Assert.That(sut.MagicPower, Is.GreaterThanOrEqualTo(1));
            Assert.That(sut.MagicPower, Is.LessThanOrEqualTo(3));
        }
    }
}
