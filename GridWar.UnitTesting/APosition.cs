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
    public class APosition
    {
        [Test]
        public void TwoPositionsWithSameXAndYShouldBeEqual()
        {
            Position position1 = new Position { X = 2, Y = 2 };
            Position position2 = new Position { X = 2, Y = 2 };
            Assert.That(position1, Is.EqualTo(position2));

            position2 = new Position { X = 3, Y = 4 };
            Assert.That(position1, Is.Not.EqualTo(position2));            
        }
    }
}
