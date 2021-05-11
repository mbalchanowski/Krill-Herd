using System;

namespace KrillHerd
{
    public sealed class RandomGenerator
    {
        static readonly RandomGenerator _instance = new RandomGenerator();
        public static RandomGenerator Instance
        {
            get
            {
                return _instance;
            }
        }
        public Random Random { get; set; }

        private RandomGenerator()
        {
            Random = new Random();
        }
    }
}
