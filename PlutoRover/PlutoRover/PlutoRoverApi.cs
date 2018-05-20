using System.Linq;
using PlutoRover.Contracts;
using PlutoRover.Models;

namespace PlutoRover
{
    // TODO: Add error handling
    // TODO: Add logging service
    // TODO: Add some RESTful API layer
    public class PlutoRoverApi : IPlutoRoverApi
    {
        private readonly IValidationService _validationService;
        private readonly ILocationService _locationService;
        private readonly IObstacleDetectionService _obstacleDetectionService;

        public PlutoRoverApi(IValidationService validationService, ILocationService locationService,
            IObstacleDetectionService obstacleDetectionService)
        {
            _validationService = validationService;
            _locationService = locationService;
            _obstacleDetectionService = obstacleDetectionService;
        }

        public PlutoRoverMoveResponse Move(string commands)
        {
            _validationService.ValidateCommand(commands);

            var currentLocation = _locationService.GetCurrentLocation();
            var surfaceSize = _locationService.GetSurfaceSize();

            Location newLocation = null;
            foreach (var command in commands.Select(character => character.ToString()))
            {
                newLocation = currentLocation.CalculateNewLocation(command, surfaceSize);

                if (_obstacleDetectionService.HasObstacle(newLocation))
                {
                    return new PlutoRoverMoveResponse(
                        currentLocation,
                        isError: true,
                        errorMessage:
                        $"Obstacle detected on location [{newLocation.X}, {newLocation.Y}, {newLocation.Direction}].");
                }

                _locationService.UpdateLocation(newLocation);

                currentLocation = newLocation;
            }

            return new PlutoRoverMoveResponse(newLocation);
        }
    }
}
