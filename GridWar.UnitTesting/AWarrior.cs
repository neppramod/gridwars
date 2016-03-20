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
    public class AWarrior
    {
        [Test]
        public void ShouldBeAbleToAddAWeaon()
        {
            var sut = new MagicWarrior();

            Assert.That(sut.hasWeapon(), Is.EqualTo(false));
            sut.addWeapon(new Weapon(WeaponType.Staff));
            Assert.That(sut.hasWeapon(), Is.EqualTo(true));
        }

        [Test]
        public void ShouldBeAbleToDropWeaon()
        {
            var sut = new MeleeWarrior();

            sut.addWeapon(new Weapon(WeaponType.Sword));
            Assert.That(sut.hasWeapon(), Is.EqualTo(true));
            sut.dropWeapon();
            Assert.That(sut.hasWeapon(), Is.EqualTo(false));
        }

        [Test]
        public void ShouldHaveHitPointsOfHundredWhenCreated()
        {
            var sut = new MagicWarrior();
            Assert.That(sut.HitPoints, Is.EqualTo(100));
        }

        [Test]
        public void ShouldHaveDefensePercentageOfZeroWhenCreated()
        {
            var sut = new MeleeWarrior();
            Assert.That(sut.DefensePercentage, Is.EqualTo(0));
        }

        [Test]
        public void ShouldIncreaseMeleePowerWhenASwordIsPicked()
        {
            var sut = new MeleeWarrior();
            double meleePowerPre = sut.MeleePower;
            var weapon = new Weapon(WeaponType.Sword);
            sut.addWeapon(weapon);

            Assert.That(sut.MeleePower, Is.EqualTo(meleePowerPre + weapon.Power));
        }

        [Test]
        public void ShouldIncreaseMagicPowerWhenAStaffIsPicked()
        {
            var sut = new MagicWarrior();
            double magicPowerPre = sut.MagicPower;
            var weapon = new Weapon(WeaponType.Staff);
            sut.addWeapon(weapon);

            Assert.That(sut.MagicPower, Is.EqualTo(magicPowerPre + weapon.Power));
        }
    }
}
