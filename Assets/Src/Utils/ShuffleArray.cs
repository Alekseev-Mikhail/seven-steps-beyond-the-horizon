using System;

namespace Utils
{
    public static class ShuffleArray
    {
        public static void Shuffle<T>(this T[] array, Random random)
        {
            Array.Sort(array, (_, _) => random.Next(-1, 2));
        }
    }
}