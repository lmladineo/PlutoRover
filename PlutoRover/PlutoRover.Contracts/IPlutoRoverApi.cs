using PlutoRover.Models;

namespace PlutoRover.Contracts
{
    public interface IPlutoRoverApi
    {
        PlutoRoverMoveResponse Move(string command);
    }
}
