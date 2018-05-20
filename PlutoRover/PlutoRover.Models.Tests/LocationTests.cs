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
        private static readonly (int width, int height) SphereSize = (100, 100);

        [TestCase("F")]
        [TestCase("B")]
        public void CalculateNewLocation_SendSupportedCommand_ResponseIsNotNull(string command)
        {
            // Arrange
            var currentLocation = new Location(0, 0, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, SphereSize);

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
            var newLocation = currentLocation.CalculateNewLocation(command, SphereSize);

            // Assert
            newLocation.Should().NotBeSameAs(currentLocation);
        }

        [TestCase(0, 1, 100)]
        [TestCase(99, 0, 100)]
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingNorth_RoverHasCorrectNewLocation(int startLocationY, int expectedNewLocationY, int sphereHeight)
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, startLocationY, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (100, sphereHeight));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == expectedNewLocationY)
                .And
                .Match<Location>(location => location.Direction == Direction.N);
        }

        [TestCase(0, 99, 100)]
        [TestCase(99, 98, 100)]
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingSouth_RoverHasCorrectNewLocation(int startLocationY, int expectedNewLocationY, int sphereHeight)
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(0, startLocationY, Direction.S);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (100, sphereHeight));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == expectedNewLocationY)
                .And
                .Match<Location>(location => location.Direction == Direction.S);
        }

        [TestCase(0, 1, 100)]
        [TestCase(99, 0, 100)]
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingEast_RoverHasCorrectNewLocation(int startLocationX, int expectedNewLocationX, int sphereWidth)
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(startLocationX, 0, Direction.E);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (sphereWidth, 100));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == expectedNewLocationX)
                .And
                .Match<Location>(location => location.Y == 0)
                .And
                .Match<Location>(location => location.Direction == Direction.E);
        }

        [TestCase(0, 99, 100)]
        [TestCase(99, 98, 100)]
        public void CalculateNewLocation_SendForwardCommandWithRoverFacingWest_RoverHasCorrectNewLocation(int startLocationX, int expectedNewLocationX, int sphereWidth)
        {
            // Arrange
            var command = "F";
            var currentLocation = new Location(startLocationX, 0, Direction.W);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (sphereWidth, 100));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == expectedNewLocationX)
                .And
                .Match<Location>(location => location.Y == 0)
                .And
                .Match<Location>(location => location.Direction == Direction.W);
        }

        [TestCase(0, 99, 100)]
        [TestCase(99, 98, 100)]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingNorth_RoverHasCorrectNewLocation(int startLocationY, int expectedNewLocationY, int sphereHeight)
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(0, startLocationY, Direction.N);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (100, sphereHeight));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == expectedNewLocationY)
                .And
                .Match<Location>(location => location.Direction == Direction.N);
        }

        [TestCase(0, 1, 100)]
        [TestCase(99, 0, 100)]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingSouth_RoverHasCorrectNewLocation(int startLocationY, int expectedNewLocationY, int sphereHeight)
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(0, startLocationY, Direction.S);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (100, sphereHeight));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == expectedNewLocationY)
                .And
                .Match<Location>(location => location.Direction == Direction.S);
        }

        [TestCase(0, 99, 100)]
        [TestCase(99, 98, 100)]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingEast_RoverHasCorrectNewLocation(int startLocationX, int expectedNewLocationX, int sphereWidth)
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(startLocationX, 0, Direction.E);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (sphereWidth, 100));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == expectedNewLocationX)
                .And
                .Match<Location>(location => location.Y == 0)
                .And
                .Match<Location>(location => location.Direction == Direction.E);
        }

        [TestCase(0, 1, 100)]
        [TestCase(99, 0, 100)]
        public void CalculateNewLocation_SendBackwardCommandWithRoverFacingWest_RoverHasCorrectNewLocation(int startLocationX, int expectedNewLocationX, int sphereWidth)
        {
            // Arrange
            var command = "B";
            var currentLocation = new Location(startLocationX, 0, Direction.W);

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command, (sphereWidth, 100));

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == expectedNewLocationX)
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
            Action action = () => currentLocation.CalculateNewLocation(command, SphereSize);

            // Assert
            action.Should().Throw<ApplicationException>();
        }

        [TestCase(Direction.N, Direction.E)]
        [TestCase(Direction.E, Direction.S)]
        [TestCase(Direction.S, Direction.W)]
        [TestCase(Direction.W, Direction.N)]
        public void CalculateNewLocation_SendRightCommand_RoverTurnsRight(Direction startDirection, Direction expectedNewDirection)
            => CalculateNewLocation_SendXCommand_RoverHasChangedDirection(
                command: "R",
                currentLocation: new Location(0, 0, startDirection),
                expectedNewDirection: expectedNewDirection
            );

        [TestCase(Direction.N, Direction.W)]
        [TestCase(Direction.W, Direction.S)]
        [TestCase(Direction.S, Direction.E)]
        [TestCase(Direction.E, Direction.N)]
        public void CalculateNewLocation_SendLeftCommand_RoverTurnsLeft(Direction startDirection, Direction expectedNewDirection)
            => CalculateNewLocation_SendXCommand_RoverHasChangedDirection(
                command: "L",
                currentLocation: new Location(0, 0, startDirection),
                expectedNewDirection: expectedNewDirection
            );

        private void CalculateNewLocation_SendXCommand_RoverHasChangedDirection(string command, Location currentLocation,
            Direction expectedNewDirection)
        {
            // Arrange

            // Act
            var newLocation = currentLocation.CalculateNewLocation(command,SphereSize);

            // Assert
            newLocation.Should()
                .Match<Location>(location => location.X == currentLocation.X)
                .And
                .Match<Location>(location => location.Y == currentLocation.Y)
                .And
                .Match<Location>(location => location.Direction == expectedNewDirection);
        }
    }
}
