namespace PlutoRover.Models
{
    public class PlutoRoverMoveResponse
    {
        public PlutoRoverMoveResponse(Location newLocation, bool isError = false, string errorMessage = null)
        {
            NewLocation = newLocation;
            IsError = isError;
            ErrorMessage = errorMessage;
        }

        public Location NewLocation { get; }

        public bool IsError { get; }

        public string ErrorMessage { get; }
    }
}
