using PlutoRover.Contracts;
using PlutoRover.Models;
using PlutoRover.Models.Enums;

namespace PlutoRover
{
    public class PlutoRoverApi : IPlutoRoverApi
    {
        public Location Move(string command)
        {
            var currentLocation = new Location(0, 0, Direction.N);

            // TODO: Implement command validation
            // TODO: Implement command parsing
            return command != "F"
                ? null
                : currentLocation.CalculateNewLocation(command);
        }
    }
}
