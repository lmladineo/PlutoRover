using PlutoRover.Models;

namespace PlutoRover.Contracts
{
    // Note: Can use DB or some external service.
    // Depending on if Rover has it's on DB or must contact external service for info on his location.
    public interface ILocationService
    {
        Location GetCurrentLocation();

        void UpdateLocation(Location newLocation);

        (int width, int height) GetSurfaceSize();
    }
}
