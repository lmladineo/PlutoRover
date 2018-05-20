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
            // TODO: Move calculation of new location to Location model
            return command != "F"
                ? null
                : new Location(currentLocation.X, currentLocation.Y + 1, currentLocation.Direction);
        }
    }
}
