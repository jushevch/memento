using System;
using System.Collections.Generic;

namespace Mem.Core
{
    public enum Direction
    {
        None,
        North,
        East,
        South,
        West,
        Up,
        Down,
        Somewhere,
        NorthEast,
        SouthEast,
        SouthWest,
        NorthWest,
    }

    public static class Director
    {
        private static readonly Dictionary<Direction, string> names = new ()
        {
            [Direction.None] = "нет",
            [Direction.North] = "север",
            [Direction.East] = "восток",
            [Direction.South] = "юг",
            [Direction.West] = "запад",
            [Direction.Up] = "вверх",
            [Direction.Down] = "вниз",
            [Direction.Somewhere] = "куда-то",
            [Direction.NorthEast] = "северо-восток",
            [Direction.SouthEast] = "юго-восток",
            [Direction.SouthWest] = "юго-запад",
            [Direction.NorthWest] = "северо-запад",
        };

        private static readonly Dictionary<string, Direction> directions = new ()
        {
            ["нет"] = Direction.None,
            ["север"] = Direction.North,
            ["восток"] = Direction.East,
            ["юг"] = Direction.South,
            ["запад"] = Direction.West,
            ["вверх"] = Direction.Up,
            ["вниз"] = Direction.Down,
            ["куда-то"] = Direction.Somewhere,
            ["северо-восток"] = Direction.NorthEast,
            ["юго-восток"] = Direction.SouthEast,
            ["юго-запад"] = Direction.SouthWest,
            ["северо-запад"] = Direction.NorthWest,
        };

        private static readonly Dictionary<Direction, Direction> opposites = new ()
        {
            [Direction.None] = Direction.None,
            [Direction.North] = Direction.South,
            [Direction.East] = Direction.West,
            [Direction.South] = Direction.North,
            [Direction.West] = Direction.East,
            [Direction.Up] = Direction.Down,
            [Direction.Down] = Direction.Up,
            [Direction.Somewhere] = Direction.Somewhere,
            [Direction.NorthEast] = Direction.SouthWest,
            [Direction.SouthEast] = Direction.NorthWest,
            [Direction.SouthWest] = Direction.NorthEast,
            [Direction.NorthWest] = Direction.SouthEast,
        };

        public static string GetName(this Direction dir)
        {
            if (names.TryGetValue(dir, out var name))
            {
                return name;
            }

            throw new ArgumentException("Invalid direction value.", nameof(dir));
        }

        public static Direction GetDirection(string name)
        {
            if (directions.TryGetValue(name, out var dir))
            {
                return dir;
            }

            throw new ArgumentException("Invalid direction name.", nameof(name));
        }

        public static Direction GetOpposite(this Direction dir)
        {
            if (opposites.TryGetValue(dir, out var opposite))
            {
                return opposite;
            }

            throw new ArgumentException("Invalid direction value.", nameof(dir));
        }
    }
}
