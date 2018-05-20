using FluentAssertions;
using NUnit.Framework;
using PlutoRover.Contracts;
using PlutoRover.Models;
using PlutoRover.Models.Enums;

namespace PlutoRover.Tests
{
    [TestFixture]
    public class PlutoRoverApiTests
    {
        private IPlutoRoverApi _plutoRoverApi;

        [SetUp]
        protected void SetUp()
        {
            _plutoRoverApi = new PlutoRoverApi();
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
    }
}
