using PlutoRover.Models;

namespace PlutoRover.Contracts
{
    public interface IObstacleDetectionService
    {
        bool HasObstacle(Location location);
    }
}
