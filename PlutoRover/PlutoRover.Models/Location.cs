using System;
using System.Collections.Generic;
using PlutoRover.Models.Enums;

namespace PlutoRover.Models
{
    public class Location
    {
        private readonly Dictionary<string, Func<Location>> _commandToNewLocation;

        public Location(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;

            _commandToNewLocation = new Dictionary<string, Func<Location>>()
            {
                {"F", MoveForward}
            };
        }

        public int X { get; }

        public int Y { get; }

        public Direction Direction { get; }

        public Location CalculateNewLocation(string command) => _commandToNewLocation.ContainsKey(command)
            ? _commandToNewLocation[command]()
            : throw new Exception("Command not supported");

        private Location MoveForward() => new Location(X, Y + 1, Direction);
    }
}
