using System;

namespace KrillHerd
{
    public class Parameters
    {
        /// <summary>
        /// Number of iterations to perform
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Number of krills
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        /// Lower bound
        /// </summary>
        public double LB { get; set; }

        /// <summary>
        /// Upper bound
        /// </summary>
        public double UB { get; set; }

        /// <summary>
        /// Constant number between [0, 2]
        /// WARNING! This parameter highly depends on problem and number of dimensions
        /// </summary>
        public double C_t { get; set; }
        public int Dimensions { get; set; }
        public KrillParameters KrillParameters { get; set; }
        public FitnessFunction FitnessFunction { get; set; }

        /// <summary>
        /// Default parameters
        /// </summary>
        public Parameters()
        {
            Dimensions = 2;
            Iterations = 250;
            Population = 50;
            C_t = 0.1; // WARNING! This parameter highly depends on problem and number of dimensions
            KrillParameters = new KrillParameters();
        }

        public void Show()
        {
            Console.WriteLine("------ PARAMETERS --------");
            Console.WriteLine("Iterations: " + Iterations);
            Console.WriteLine("Population: " + Population);
            Console.WriteLine("C_t: " + C_t);
            Console.WriteLine("Dimensions: " + Dimensions);
            Console.WriteLine("N_Max: " + KrillParameters.N_Max);
            Console.WriteLine("V_f: " + KrillParameters.V_f);
            Console.WriteLine("D_max: " + KrillParameters.D_max);
            Console.WriteLine("--------------------------");
        }
    }

    public class KrillParameters
    {
        public double N_Max { get; set; }
        public double V_f { get; set; }
        public double D_max { get; set; }

        /// <summary>
        /// Default parameters
        /// </summary>
        public KrillParameters()
        {
            N_Max = 0.01;
            V_f = 0.02;
            D_max = 0.02;
        }
    }
}