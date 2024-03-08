
using System;
using System.Linq;

public static class ArrayExtensions
{
    // Add another 2D array to the current 2D array
    public static int[,] Add(this int[,] source , int[,] arrayToAdd)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);

        int[,] result = new int[rows , cols];

        for (int i = 0; i<rows; i++)
        {
            for (int j = 0; j<cols; j++)
            {
                result[i , j]=source[i , j]+arrayToAdd[i , j];
            }
        }

        return result;
    }

    // Remove a 2D array from the current 2D array based on content
    public static int[,] Remove(this int[,] source , int[,] arrayToRemove)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);

        int[,] result = new int[rows , cols];

        for (int i = 0; i<rows; i++)
        {
            for (int j = 0; j<cols; j++)
            {
                result[i , j]=source[i , j]-arrayToRemove[i , j];
            }
        }

        return result;
    }
    public static bool Contains(this int[,] source , int[,] arrayToCheck)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);

        for (int i = 0; i<rows; i++)
        {
            for (int j = 0; j<cols; j++)
            {
                if (arrayToCheck.Cast<int>().Any(value => value==source[i , j]))
                {
                    return true;
                }
            }
        }

        return false;
    }
    public static void Display(this int[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
