using PlutoRover.Contracts;
using PlutoRover.Models;

namespace PlutoRover
{
    // TODO: Add error handling
    public class PlutoRoverApi : IPlutoRoverApi
    {
        private readonly IValidationService _validationService;
        private readonly ILocationService _locationService;

        public PlutoRoverApi(IValidationService validationService, ILocationService locationService)
        {
            _validationService = validationService;
            _locationService = locationService;
        }

        public Location Move(string command)
        {
            _validationService.ValidateCommand(command);

            var currentLocation = _locationService.GetCurrentLocation();

            // TODO: Implement command parsing

            var newLocation = currentLocation.CalculateNewLocation(command);

            _locationService.UpdateLocation(newLocation);

            return newLocation;
        }
    }
}
