using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace KrillHerd
{
    public class Core
    {
        public Parameters Parameters { get; set; }
        public KrillPopulation Population { get; set; }

        public Core(Parameters parameters)
        {
            Parameters = parameters;
        }

        private void PopulationInitialization()
        {
            // Population initialization
            List<Krill> Krills = new List<Krill>();
            for (int i = 1; i <= Parameters.Population; i++)
            {
                Krill krill = new Krill(i)
                {
                    Coordinates = Vector<double>.Build.Random(Parameters.Dimensions, new ContinuousUniform(Parameters.LB, Parameters.UB))
                };
                Krills.Add(krill);
            }

            Population = new KrillPopulation(Krills);
        }

        public void KrillHerd_Run()
        {
            // Create random population
            PopulationInitialization();

            // Convert upper bound (UB) and lower bound (LB) to vector
            var UB_vector = Vector<double>.Build.Dense(Parameters.Dimensions, Parameters.UB);
            var LB_vector = Vector<double>.Build.Dense(Parameters.Dimensions, Parameters.LB);

            // Algorithm initialization
            var KrillAlgorithm = new KrillHerdAlgorithm(Population, Parameters.FitnessFunction, LB_vector, UB_vector);

            // Connecting events to the algorithm
            KrillAlgorithm.OnGenerationComplete += Krill_OnGenerationComplete;
            KrillAlgorithm.OnRunComplete += Krill_OnRunComplete;

            // Run
            KrillAlgorithm.Run(Parameters.Iterations, Parameters.C_t, Parameters.KrillParameters.N_Max, Parameters.KrillParameters.V_f, Parameters.KrillParameters.D_max);
        }

        void Krill_OnGenerationComplete(object sender, RunEventArgs e)
        {
            KrillPopulation population = (KrillPopulation)sender;
            var bestKrill = population.GetBestKrill();
            Console.WriteLine("Iteration: {0}, Fitness: {1}, Coordinates: ({2}, {3})", 
                e.Iteration, Math.Round(bestKrill.Fitness, 4), Math.Round(bestKrill.Coordinates[0], 4), Math.Round(bestKrill.Coordinates[1], 4));
        }

        void Krill_OnRunComplete(object sender, RunEventArgs e)
        {
            KrillPopulation population = (KrillPopulation)sender;
            Console.WriteLine("Run complete");
        }
    }
}
