using System;

public class BoatMovements
{
    public static bool CanTravelTo(bool[,] gameMatrix, int fromRow, int fromColumn, int toRow, int toColumn)
    {
        // Bounds checking
        if (toColumn < 0 || toRow < 0)
        {
            return false;
        }
        if (toColumn >= gameMatrix.GetLength(0) || toRow >= gameMatrix.GetLength(1))
        {
            return false;
        }
        if (fromColumn < 0 || fromRow < 0)
        {
            return false;
        }
        if (fromColumn >= gameMatrix.GetLength(0) || fromRow >= gameMatrix.GetLength(1))
        {
            return false;
        }
        int rowDist = Math.Abs(toRow - fromRow);
        int columnDist = Math.Abs(toColumn - fromColumn);

        if (rowDist > 1 || columnDist > 2)
        {
            return false;
        }
        if (rowDist > 0 && columnDist > 0)
        {
            return false;
        }

        if (columnDist == 2 && toColumn < fromColumn)
        {
            return false;
        }
        if (columnDist == 2)
        {
            if (!gameMatrix[fromRow, fromColumn + 1])
            {
                return false;
            }
        }
        return gameMatrix[toRow, toColumn];
    }

    //public static void Main()
    //{
    //    bool[,] gameMatrix =
    //    {
    //        {false, true,  true,  false, false, false},
    //        {true,  true,  true,  false, false, false},
    //        {true,  true,  true,  true,  true,  true},
    //        {false, true,  true,  false, true,  true},
    //        {false, true,  true,  true,  false, true},
    //        {false, false, false, false, false, false},
    //    };

    //    Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 2, 2)); // true, Valid move
    //    Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 3, 4)); // false, Can't travel through land
    //    Console.WriteLine(CanTravelTo(gameMatrix, 3, 2, 6, 2)); // false, Out of bounds
    //}
}