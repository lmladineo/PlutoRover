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

        [SetUp]
        protected void SetUp()
        {
            _validationServiceMock = new Mock<IValidationService>();

            _plutoRoverApi = new PlutoRoverApi(_validationServiceMock.Object);
        }

        [Test]
        public void Move_SendOneCommand_ResponseIsNotNull()
        {
            // Arrange
            var command = "F";

            // Act
            var result = _plutoRoverApi.Move(command);

            // Assert
            result.Should().NotBeNull();
        }

        [Test]
        public void Move_SendForwardCommand_RoverMovesForward()
        {
            // Arrange
            var command = "F";

            // Act
            var result = _plutoRoverApi.Move(command);

            // Assert
            result.Should()
                .Match<Location>(location => location.X == 0)
                .And
                .Match<Location>(location => location.Y == 1)
                .And
                .Match<Location>(location => location.Direction == Direction.N);
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
    }
}
