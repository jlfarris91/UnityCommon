using Random = UnityEngine.Random;

namespace CommonLib
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Wraps the System.Random class
    /// </summary>
    internal static class RandomEx
    {
        private static readonly Stack<Random.State> SeedStack = new Stack<Random.State>();

        public static void PushSeed(int seed)
        {
            if (RandomEx.SeedStack.Count == 0)
            {
                RandomEx.SeedStack.Push(Random.state);
            }

            Random.InitState(seed);
            RandomEx.SeedStack.Push(Random.state);
        }

        public static void PushSeed(Random.State state)
        {
            if (RandomEx.SeedStack.Count == 0)
            {
                RandomEx.SeedStack.Push(Random.state);
            }

            Random.state = state;
            RandomEx.SeedStack.Push(Random.state);
        }

        public static void PopSeed()
        {
            RandomEx.SeedStack.Pop();

            if (RandomEx.SeedStack.Any())
            {
                Random.state = RandomEx.SeedStack.Peek();
            }
        }

        public static bool Bool
        {
            get { return Random.Range(0, 2) == 1; }
        }
    }
}
