using FluentAssertions;
using NUnit.Framework;
using PlutoRover.Models.Enums;

namespace PlutoRover.Models.Tests
{
    [TestFixture]
    public class LocationTests
    {
        [Test]
        public void CalculateNewLocation_SendForwardCommand_ResponseIsNotNull()
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, 0, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should().NotBeNull();
        }

        [Test]
        public void CalculateNewLocation_SendForwardCommand_ResponseIsNewLocationObject()
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, 0, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should().NotBeSameAs(currentLocation);
        }

        [Test]
        public void CalculateNewLocation_SendForwardCommand_RoverMovesForward()
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, 0, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == 1)
                .And
                .Match<Location>(location => location.Direction == Direction.N);
        }
    }
}
