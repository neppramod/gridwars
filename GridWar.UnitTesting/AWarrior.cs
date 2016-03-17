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
        public void ShouldMoveAWarriorInADirection()
        {
            var sut = new MeleeWorrior();

            sut.Position = new Position { X = 0, Y = 0 };
            int prePositionX = sut.Position.X;
            int prePositionY = sut.Position.Y;

            sut.Move(Direction.EAST); 
            Assert.That(sut.Position.Y, Is.EqualTo(prePositionY + 1));

            sut.Move(Direction.SOUTH);
            Assert.That(sut.Position.X, Is.EqualTo(prePositionX + 1));

            prePositionX = sut.Position.X;
            prePositionY = sut.Position.Y;
            sut.Move(Direction.EAST_SOUTH);
            Assert.That(sut.Position.Y, Is.EqualTo(prePositionY + 1));
            Assert.That(sut.Position.X, Is.EqualTo(prePositionX + 1));            

            prePositionX = sut.Position.X;
            prePositionY = sut.Position.Y;
            sut.Move(Direction.NORTH);
            sut.Move(Direction.EAST_NORTH);
            Assert.That(sut.Position.X, Is.EqualTo(prePositionX - 2));
            Assert.That(sut.Position.Y, Is.EqualTo(prePositionY + 1));
            
        }

        [Test]
        public void ShouldNotBeAbleToMoveAWarriorPastABoundary()
        {
            var sut = new MeleeWorrior();

            // Top left
            sut.Position = new Position { X = 0, Y = 0 };
            Assert.That(() => sut.Move(Direction.WEST), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.NORTH), Throws.TypeOf<InvalidOperationException>());            
            Assert.That(() => sut.Move(Direction.EAST_NORTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.WEST_SOUTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.WEST_NORTH), Throws.TypeOf<InvalidOperationException>());

            // Bottom right
            sut.Position = new Position { X = Board.BOARD_SIZE - 1, Y = Board.BOARD_SIZE - 1 };
            Assert.That(() => sut.Move(Direction.SOUTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.EAST), Throws.TypeOf<InvalidOperationException>());            
            Assert.That(() => sut.Move(Direction.EAST_SOUTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.WEST_SOUTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.EAST_NORTH), Throws.TypeOf<InvalidOperationException>());
            
            // Top right
            sut.Position = new Position { X = 0, Y = Board.BOARD_SIZE - 1 };
            Assert.That(() => sut.Move(Direction.EAST), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.NORTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.EAST_SOUTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.WEST_NORTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.EAST_NORTH), Throws.TypeOf<InvalidOperationException>());
                        
            // Bottom left
            sut.Position = new Position { X = Board.BOARD_SIZE - 1, Y = 0 };
            Assert.That(() => sut.Move(Direction.WEST), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.SOUTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.EAST_SOUTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.WEST_NORTH), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => sut.Move(Direction.WEST_SOUTH), Throws.TypeOf<InvalidOperationException>());
            
        }

        [Test]
        public void ShouldBeAbleToAddAWeaon()
        {
            var sut = new MagicWorrior();

            Assert.That(sut.hasWeapon(), Is.EqualTo(false));
            sut.addWeapon(new Weapon(WeaponType.Staff));
            Assert.That(sut.hasWeapon(), Is.EqualTo(true));
        }

        [Test]
        public void ShouldBeAbleToDropWeaon()
        {
            var sut = new MeleeWorrior();

            sut.addWeapon(new Weapon(WeaponType.Sword));
            Assert.That(sut.hasWeapon(), Is.EqualTo(true));
            sut.dropWeapon();
            Assert.That(sut.hasWeapon(), Is.EqualTo(false));
        }
    }
}
