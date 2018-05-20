﻿using System;
using System.Collections.Generic;
using PlutoRover.Models.Enums;
using ApplicationException = PlutoRover.Models.Exceptions.ApplicationException;

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

            _commandToNewLocation = new Dictionary<string, Func<Location>>();

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

        public Location CalculateNewLocation(string command) => _commandToNewLocation.ContainsKey(command)
            ? _commandToNewLocation[command]()
            : throw new ApplicationException($"Command '{command}' not supported.");

        private Location MoveNorth() => new Location(X, Y + 1, Direction);

        private Location MoveEast() => new Location(X + 1, Y, Direction);

        private Location MoveSouth() => new Location(X, Y - 1, Direction);

        private Location MoveWest() => new Location(X - 1, Y, Direction);
    }
}
