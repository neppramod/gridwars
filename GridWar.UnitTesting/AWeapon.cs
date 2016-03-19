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
    public class AWeapon
    {
        [Test]
        public void ShouldSetPowerBetweenThreeAndSixForStaffWeaponTypeWhenConstructed()
        {
            var sut = new Weapon(WeaponType.Staff);
            Assert.That(sut.Power, Is.GreaterThanOrEqualTo(3));
            Assert.That(sut.Power, Is.LessThanOrEqualTo(6));
        }

        [Test]
        public void ShouldSetPowerBetweenTwoAndFourForSwordWeaponTypeWhenConstructed()
        {
            var sut = new Weapon(WeaponType.Sword);
            Assert.That(sut.Power, Is.GreaterThanOrEqualTo(2));
            Assert.That(sut.Power, Is.LessThanOrEqualTo(4));
        }
    }
}
