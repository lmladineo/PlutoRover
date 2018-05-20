using FluentAssertions;
using NUnit.Framework;
using PlutoRover.Models.Enums;
using PlutoRover.Models.Extensions;

namespace PlutoRover.Models.Tests
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [TestCase(Direction.N, Direction.E)]
        [TestCase(Direction.E, Direction.S)]
        [TestCase(Direction.S, Direction.W)]
        [TestCase(Direction.W, Direction.N)]
        public void Next_DirectionEnum_Moves90DegreesClockwise(Direction startDirection, Direction expectedEndDirection)
        {
            // Arrange

            // Act
            var actualEndDirection = startDirection.Next();

            // Assert
            actualEndDirection.Should().Be(expectedEndDirection);
        }

        [TestCase(Direction.N, Direction.W)]
        [TestCase(Direction.W, Direction.S)]
        [TestCase(Direction.S, Direction.E)]
        [TestCase(Direction.E, Direction.N)]
        public void Previous_DirectionEnum_Moves90DegreesCounterClockwise(Direction startDirection, Direction expectedEndDirection)
        {
            // Arrange

            // Act
            var actualEndDirection = startDirection.Previous();

            // Assert
            actualEndDirection.Should().Be(expectedEndDirection);
        }
    }
}
