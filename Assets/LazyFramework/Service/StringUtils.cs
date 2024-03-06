using System.Collections.Generic;
using System;

namespace LazyFramework
{
    public static class StringUtils
    {
        public static List<int> StringToIntList(string inputString)
        {
            List<int> intList = new List<int>();

            foreach (char digitChar in inputString)
            {
                // Try to parse each character to an integer
                if (int.TryParse(digitChar.ToString() , out int result))
                {
                    intList.Add(result);
                }
                else
                {
                    Console.WriteLine($"Failed to parse '{digitChar}' as an integer.");
                }
            }

            return intList;
        }
    }
}


