using System;

public class RoutePlanner
{
    public struct Coordinate
    {
        public int row, col;

        public Coordinate(int row, int col)
        {
            this.row = row;
            this.col = col;

        }

        public override bool Equals(object? obj)
        {
            if (obj is Coordinate c)
            {
                return row == c.row && col == c.col;
            }
            return false;
        }

        public override string ToString()
        {
            return $"R:{row} C:{col}";
        }

        public override int GetHashCode()
        {
            return row.GetHashCode() ^ col.GetHashCode();
        }
    }

    public static bool CoordinateInBoundsAndTraversible(Coordinate c, bool[,] map)
    {
        // bounds checks
        if (c.row < 0 || c.row >= map.GetLength(0))
        {
            return false;
        }
        if (c.col < 0 || c.col >= map.GetLength(1))
        {
            return false;
        }
        return map[c.row, c.col];
    }

    public static bool RecurseRoutePossible(Coordinate current, Coordinate to, bool[,] map, bool[,] visited)
    {
        if (current.Equals(to)) return true;
        visited[current.row, current.col] = true;

        Coordinate down = current;
        down.row += 1;

        Coordinate right = current;
        right.col += 1;

        Coordinate up = current;
        up.row -= 1;

        Coordinate left = current;
        left.col -= 1;

        bool canMoveDown = CoordinateInBoundsAndTraversible(down, map);
        bool canMoveRight = CoordinateInBoundsAndTraversible(right, map);
        bool canMoveUp = CoordinateInBoundsAndTraversible(up, map);
        bool canMoveLeft = CoordinateInBoundsAndTraversible(left, map);

        bool result = false;
        if (canMoveDown && !visited[down.row, down.col])
        {
            result = result || RecurseRoutePossible(down, to, map, visited);
        }
        if (canMoveRight && !visited[right.row, right.col])
        {
            result = result || RecurseRoutePossible(right, to, map, visited);
        }
        if (canMoveLeft && !visited[left.row, left.col])
        {
            result = result || RecurseRoutePossible(left, to, map, visited);
        }
        if (canMoveUp && !visited[up.row, up.col])
        {
            result = result || RecurseRoutePossible(up, to, map, visited);
        }

        return result;
    }

    public static bool RouteExists(int fromRow, int fromColumn, int toRow, int toColumn,
                                  bool[,] mapMatrix)
    {
        Coordinate from = new Coordinate(fromRow, fromColumn);
        Coordinate to = new Coordinate(toRow, toColumn);
        bool[,] visited = new bool[mapMatrix.GetLength(0), mapMatrix.GetLength(1)];

        return RecurseRoutePossible(from, to, mapMatrix, visited);
    }

    //public static void Main(string[] args)
    //{
    //    bool[,] mapMatrix = {
    //        {true, false, false},
    //        {true, true, false},
    //        {false, true, true}
    //    };

    //    Console.WriteLine(RouteExists(0, 0, 2, 2, mapMatrix));
    //}
}