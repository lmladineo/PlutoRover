using PlutoRover.Contracts;
using PlutoRover.Models;
using PlutoRover.Models.Enums;

namespace PlutoRover
{
    // TODO: Add error handling
    public class PlutoRoverApi : IPlutoRoverApi
    {
        private readonly IValidationService _validationService;

        public PlutoRoverApi(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public Location Move(string command)
        {
            _validationService.ValidateCommand(command);

            var currentLocation = new Location(0, 0, Direction.N);

            // TODO: Implement command parsing

            return currentLocation.CalculateNewLocation(command);
        }
    }
}
