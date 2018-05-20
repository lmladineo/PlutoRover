using System;
using System.Collections.Generic;
using PlutoRover.Models.Enums;
using PlutoRover.Models.Extensions;
using ApplicationException = PlutoRover.Models.Exceptions.ApplicationException;

namespace PlutoRover.Models
{
    public class Location
    {
        private readonly Dictionary<string, Func<(int width, int height), Location>> _commandToNewLocation;

        public Location(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;

            _commandToNewLocation = new Dictionary<string, Func<(int width, int height), Location>>
            {
                {"R", TurnRight},
                {"L", TurnLeft},
            };

            switch (direction)
            {
                case Direction.N:
                    _commandToNewLocation.Add("F", MoveNorth);
                    _commandToNewLocation.Add("B", MoveSouth);
                    break;
                case Direction.E:
                    _commandToNewLocation.Add("F", MoveEast);
                    _commandToNewLocation.Add("B", MoveWest);
                    break;
                case Direction.S:
                    _commandToNewLocation.Add("F", MoveSouth);
                    _commandToNewLocation.Add("B", MoveNorth);
                    break;
                case Direction.W:
                    _commandToNewLocation.Add("F", MoveWest);
                    _commandToNewLocation.Add("B", MoveEast);
                    break;
            }
        }

        public int X { get; }

        public int Y { get; }

        public Direction Direction { get; }

        public Location CalculateNewLocation(string command, (int width, int height) surfaceSize) => _commandToNewLocation.ContainsKey(command)
            ? _commandToNewLocation[command](surfaceSize)
            : throw new ApplicationException($"Command '{command}' not supported.");

        private Location MoveNorth((int width, int height) surfaceSize) =>
            new Location(X, Y + 1 < surfaceSize.height ? Y + 1 : 0, Direction);

        private Location MoveEast((int width, int height) surfaceSize) =>
            new Location(X + 1 < surfaceSize.width ? X + 1 : 0, Y, Direction);

        private Location MoveSouth((int width, int height) surfaceSize) =>
            new Location(X, Y - 1 > 0 ? Y - 1 : surfaceSize.height - 1, Direction);

        private Location MoveWest((int width, int height) surfaceSize) =>
            new Location(X - 1 > 0 ? X - 1 : surfaceSize.width - 1, Y, Direction);

        private Location TurnRight((int width, int height) surfaceSize) => new Location(X, Y, Direction.Next());

        private Location TurnLeft((int width, int height) surfaceSize) => new Location(X, Y, Direction.Previous());
    }
}
