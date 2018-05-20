using PlutoRover.Models.Enums;

namespace PlutoRover.Models
{
    public class Location
    {
        public Location(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; }

        public int Y { get; }

        public Direction Direction { get; }
    }
}
