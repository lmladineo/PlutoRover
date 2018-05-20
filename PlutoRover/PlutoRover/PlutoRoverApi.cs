using PlutoRover.Contracts;
using PlutoRover.Models;

namespace PlutoRover
{
    // TODO: Add error handling
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

        public PlutoRoverMoveResponse Move(string command)
        {
            _validationService.ValidateCommand(command);

            var currentLocation = _locationService.GetCurrentLocation();

            // TODO: Implement command parsing

            var newLocation = currentLocation.CalculateNewLocation(command, _locationService.GetSurfaceSize());

            if (_obstacleDetectionService.HasObstacle(newLocation))
            {
                return new PlutoRoverMoveResponse(
                    currentLocation,
                    isError: true,
                    errorMessage:
                    $"Obstacle detected on location [{newLocation.X}, {newLocation.Y}, {newLocation.Direction}].");
            }

            _locationService.UpdateLocation(newLocation);

            return new PlutoRoverMoveResponse(newLocation);
        }
    }
}
