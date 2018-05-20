using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlutoRover.Contracts;
using PlutoRover.Models;
using PlutoRover.Models.Enums;
using PlutoRover.Models.Exceptions;

namespace PlutoRover.Tests
{
    [TestFixture]
    public class PlutoRoverApiTests
    {
        private IPlutoRoverApi _plutoRoverApi;
        private Mock<IValidationService> _validationServiceMock;
        private Mock<ILocationService> _locationServiceMock;
        private Mock<IObstacleDetectionService> _obstacleDetectionServiceMock;

        [SetUp]
        protected void SetUp()
        {
            _validationServiceMock = new Mock<IValidationService>();
            _locationServiceMock = new Mock<ILocationService>();
            _obstacleDetectionServiceMock = new Mock<IObstacleDetectionService>();

            _plutoRoverApi = new PlutoRoverApi(_validationServiceMock.Object, _locationServiceMock.Object,
                _obstacleDetectionServiceMock.Object);
        }

        [Test]
        public void Move_SendOneCommand_ResponseIsNotNull()
        {
            // Arrange
            var command = "F";
            _locationServiceMock.Setup(x => x.GetCurrentLocation()).Returns(new Location(0, 0, Direction.N));
            _locationServiceMock.Setup(x => x.GetSurfaceSize()).Returns((100, 100));

            // Act
            var result = _plutoRoverApi.Move(command);

            // Assert
            result.Should().NotBeNull();
            result?.NewLocation.Should().NotBeNull();
        }

        [Test]
        public void Move_SendForwardCommand_RoverMovesForward()
        {
            // Arrange
            var command = "F";
            _locationServiceMock.Setup(x => x.GetCurrentLocation()).Returns(new Location(0, 0, Direction.N));
            _locationServiceMock.Setup(x => x.GetSurfaceSize()).Returns((100, 100));

            // Act
            var result = _plutoRoverApi.Move(command);

            // Assert
            result.Should()
                .Match<PlutoRoverMoveResponse>(response => response.NewLocation.X == 0)
                .And
                .Match<PlutoRoverMoveResponse>(response => response.NewLocation.Y == 1)
                .And
                .Match<PlutoRoverMoveResponse>(response => response.NewLocation.Direction == Direction.N);
        }

        [Test]
        public void Move_ValidationFails_ValidationExceptionThrown()
        {
            // Arrange
            var command = "";
            _validationServiceMock.Setup(x => x.ValidateCommand(It.IsAny<string>())).Throws<ValidationException>();

            // Act
            Action action = () => _plutoRoverApi.Move(command);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Test]
        public void Move_SendForwardCommand_NoObstacleDetected()
        {
            // Arrange
            var command = "F";
            _locationServiceMock.Setup(x => x.GetCurrentLocation()).Returns(new Location(0, 0, Direction.N));
            _locationServiceMock.Setup(x => x.GetSurfaceSize()).Returns((100, 100));
            _obstacleDetectionServiceMock.Setup(x => x.HasObstacle(It.IsAny<Location>())).Returns(false);

            // Act
            var result = _plutoRoverApi.Move(command);

            // Assert
            result.Should()
                .Match<PlutoRoverMoveResponse>(response => response.IsError == false)
                .And
                .Match<PlutoRoverMoveResponse>(response => string.IsNullOrEmpty(response.ErrorMessage));
        }

        [Test]
        public void Move_SendForwardCommand_ObstacleDetected()
        {
            // Arrange
            var command = "F";
            _locationServiceMock.Setup(x => x.GetCurrentLocation()).Returns(new Location(0, 0, Direction.N));
            _locationServiceMock.Setup(x => x.GetSurfaceSize()).Returns((100, 100));
            _obstacleDetectionServiceMock.Setup(x => x.HasObstacle(It.IsAny<Location>())).Returns(true);

            // Act
            var result = _plutoRoverApi.Move(command);

            // Assert
            result.Should().Match<PlutoRoverMoveResponse>(response => response.IsError == true)
                .And
                .Match<PlutoRoverMoveResponse>(response => !string.IsNullOrEmpty(response.ErrorMessage));
        }
    }
}
