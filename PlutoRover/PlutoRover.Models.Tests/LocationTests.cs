using System;
using FluentAssertions;
using NUnit.Framework;
using PlutoRover.Models.Enums;
using ApplicationException = PlutoRover.Models.Exceptions.ApplicationException;

namespace PlutoRover.Models.Tests
{
    [TestFixture]
    public class LocationTests
    {
        [TestCase("F")]
        [TestCase("B")]
        public void CalculateNewLocation_SendSupportedCommand_ResponseIsNotNull(string command)
        {
            // Arrange
            var currentLocation = new Location(0, 0, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should().NotBeNull();
        }

        [TestCase("F")]
        [TestCase("B")]
        public void CalculateNewLocation_SendSupportedCommand_ResponseIsNewLocationObject(string command)
        {
            // Arrange
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

        [Test]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingNorth_RoverMovesBackwardWithRoverFacingNorth()
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(0, 0, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == -1)
                .And
                .Match<Location>(location => location.Direction == Direction.N);
        }

        [Test]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingEast_RoverMovesBackwardWithRoverFacingEast()
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(0, 0, Direction.E);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == -1)
                .And
                .Match<Location>(location => location.Y == 0)
                .And
                .Match<Location>(location => location.Direction == Direction.E);
        }

        [Test]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingSouth_RoverMovesBackwardWithRoverFacingSouth()
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(0, 0, Direction.S);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == 1)
                .And
                .Match<Location>(location => location.Direction == Direction.S);
        }

        [Test]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingWest_RoverMovesBackwardWithRoverFacingWest()
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(0, 0, Direction.W);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 1)
                .And
                .Match<Location>(location => location.Y == 0)
                .And
                .Match<Location>(location => location.Direction == Direction.W);
        }

        [Test]
        public void CalculateNewLocation_SendNonSupportedCommand_ApplicationExceptionThrown()
        {
            // Arrange
            var command = "X";
            var currentLocation = new Location(0, 0, Direction.W);

            // Act
            Action action = () => currentLocation.CalculateNewLocation(command);

            // Assert
            action.Should().Throw<ApplicationException>();
        }
    }
}
