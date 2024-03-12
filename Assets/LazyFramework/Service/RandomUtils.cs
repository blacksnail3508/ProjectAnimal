using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LazyFramework
{
    public static class RandomUtils
    {
        public static int RandomNumberSign()
        {
            return Random.Range(0 , 100)<50 ? -1 : 1;
        }
        public static Vector3 RandomDirection()
        {
            Vector3 direction = new Vector3();
            while (direction.x*direction.x+direction.y*direction.y==0)
            {
                float x = Random.Range(-2 , 2);
                float y = Random.Range(-2 , 2);
                float z = Random.Range(-2 , 2);
                direction=new Vector3(x , y , z);
            }
            return direction;
        }
        public static float RandomRange(float min , float max)
        {
            return Random.Range(min , max);
        }
        public static int RandomInSpecificRange(int[] range)
        {
            int[] specificNumbers = range;

            // Get a random number from the specific set
            int randomIndex = Random.Range(0 , specificNumbers.Length);
            int result = specificNumbers[randomIndex];
            return result;
        }
        public static int RandomInSpecificRange(List<int> range)
        {
            List<int> specificNumbers = range;

            // Get a random number from the specific set
            int randomIndex = Random.Range(0 , specificNumbers.Count);

            int result = specificNumbers[randomIndex];
            return result;
        }

        public static int RandomWithWeight(List<int> items , List<int> weights)
        {
            if (items==null||weights==null||items.Count==0||weights.Count==0||items.Count!=weights.Count)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            int totalWeight = 0;

            foreach (int weight in weights)
            {
                if (weight<0)
                {
                    throw new ArgumentException("Weights cannot be negative.");
                }

                totalWeight+=weight;
            }

            int randomNumber = UnityEngine.Random.Range(1 , totalWeight+1);

            for (int i = 0; i<weights.Count; i++)
            {
                if (randomNumber<=weights[i])
                {
                    return items[i];
                }

                randomNumber-=weights[i];
            }

            // This line should not be reached unless there is an issue with the weights
            throw new InvalidOperationException("Weighted random item calculation failed.");
        }
        public static int RandomWithWeight(int[] items , int[] weights)
        {
            if (items==null||weights==null||items.Length==0||weights.Length==0||items.Length!=weights.Length)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            int totalWeight = 0;

            foreach (int weight in weights)
            {
                if (weight<0)
                {
                    throw new ArgumentException("Weights cannot be negative.");
                }

                totalWeight+=weight;
            }

            int randomNumber = UnityEngine.Random.Range(1 , totalWeight+1);

            for (int i = 0; i<weights.Length; i++)
            {
                if (randomNumber<=weights[i])
                {
                    return items[i];
                }

                randomNumber-=weights[i];
            }

            // This line should not be reached unless there is an issue with the weights
            throw new InvalidOperationException("Weighted random item calculation failed.");
        }
        public static bool RandomRate(int rate)
        {
            int rand = Random.Range(0, 100);

            if(rand > rate)
            {
                return false;
            }

            return true;
        }
    }
}
