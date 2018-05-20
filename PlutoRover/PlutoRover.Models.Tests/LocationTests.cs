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
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingNorth_RoverMovesForwardWithRoverFacingNorth()
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

        [Test]
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingEast_RoverMovesForwardWithRoverFacingEast()
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, 0, Direction.E);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 1)
                .And
                .Match<Location>(location => location.Y == 0)
                .And
                .Match<Location>(location => location.Direction == Direction.E);
        }

        [Test]
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingSouth_RoverMovesForwardWithRoverFacingSouth()
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, 0, Direction.S);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == -1)
                .And
                .Match<Location>(location => location.Direction == Direction.S);
        }


        [Test]
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingWest_RoverMovesForwardWithRoverFacingWest()
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, 0, Direction.W);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == -1)
                .And
                .Match<Location>(location => location.Y == 0)
                .And
                .Match<Location>(location => location.Direction == Direction.W);
        }
    }
}
