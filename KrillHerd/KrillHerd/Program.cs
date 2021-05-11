using System;

namespace KrillHerd
{
    class Program
    {
        static void Main(string[] args)
        {
            Parameters parameters = new Parameters
            {
                Dimensions = 2,
                FitnessFunction = TestFunctions.RastriginFunction,
                UB = TestFunctions.RastriginDomain.Item2,
                LB = TestFunctions.RastriginDomain.Item1,
                C_t = 0.1 // This parameter highly depends on problem and number of dimensions
            };
            parameters.Show();

            Core core = new Core(parameters);
            core.KrillHerd_Run();

            Console.ReadLine();
        }
    }
}
