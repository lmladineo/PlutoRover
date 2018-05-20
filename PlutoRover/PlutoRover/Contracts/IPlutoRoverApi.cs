using PlutoRover.Models;

namespace PlutoRover.Contracts
{
    public interface IPlutoRoverApi
    {
        Location Move(string command);
    }
}
