using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace KrillHerd
{
    public class InducedMotion : MotionBase
    {
        private double N_max = 0;
        public int MaxIteration;
        private Neighbourhood Neighbourhood;
        private Dictionary<Tuple<int, int>, Vector<double>> InducedSpeedHistory;

        public InducedMotion(KrillPopulation KrillPopulation, FitnessFunction fitnessFunction, int maxIteration, double N_max) 
            : base(KrillPopulation, fitnessFunction)
        {
            this.KrillPopulation = KrillPopulation;
            Neighbourhood = new Neighbourhood(this.KrillPopulation);
            InducedSpeedHistory = new Dictionary<Tuple<int, int>, Vector<double>>();
            MaxIteration = maxIteration;
            this.N_max = N_max;
        }

        /// <summary>
        /// EQUATION 2
        /// </summary>
        public Vector<double> GetInducedMotion(Krill krill, int currentIteration)
        {
            int lastIteration = currentIteration - 1;

            Vector<double> Alpha_i = MotionDirection(krill, currentIteration);
            Vector<double> N_old = InducedSpeedHistory.ContainsKey(new Tuple<int, int>(lastIteration, krill.KrillNumber)) 
                ? InducedSpeedHistory[new Tuple<int, int>(lastIteration, krill.KrillNumber)] 
                : Vector<double>.Build.Dense(krill.Coordinates.Count);

            double Omega_n = (0.1 + (0.8 * (1 - currentIteration / MaxIteration)));
            Vector<double> N_new = N_max * Alpha_i + Omega_n * N_old;

            // We add a krill to the history to know what the value of N_old is in later iterations
            InducedSpeedHistory.Add(new Tuple<int, int>(currentIteration, krill.KrillNumber), N_new);

            return N_new;
        }

        /// <summary>
        /// EQUATION 3
        /// </summary>
        private Vector<double> MotionDirection(Krill krill, int currentIteration)
        {
            Vector<double> alpha_local = AlphaLocal(krill); // i-th Krill
            Vector<double> alpha_target = AlphaTarget(krill, currentIteration); // i-th Krill
            
            return alpha_local + alpha_target;
        }

        /// <summary>
        /// EQUATION 4
        /// </summary>
        private Vector<double> AlphaLocal(Krill krill)
        {
            Vector<double> alphaLocal = Vector<double>.Build.Dense(krill.Coordinates.Count);

            var neighbourhood = Neighbourhood.GetNeighbourhood(krill);
            if (neighbourhood.Count == 0)
                return alphaLocal;

            foreach (var neighbour in neighbourhood) // NN
            {
                var K_i_j_value = K_i_j(krill.Fitness, neighbour.Fitness);
                alphaLocal = alphaLocal.Add(X_i_j(krill.Coordinates, neighbour.Coordinates).Multiply(K_i_j_value)); // i-th Krill, j-th Krill Neighbour
            }

            return alphaLocal;
        }

        /// <summary>
        /// EQUATION 8
        /// </summary>
        private Vector<double> AlphaTarget(Krill krill, int currentIteration)
        {
            Krill bestKrill = KrillPopulation.GetBestKrill();

            if (bestKrill.Fitness > krill.Fitness)
            {
                double K_i_best = K_i_j(krill.Fitness, bestKrill.Fitness);
                Vector<double> X_i_best = X_i_j(krill.Coordinates, bestKrill.Coordinates);
                double C_best = EffectiveCoefficient(currentIteration);

                return X_i_best.Multiply(C_best * K_i_best);
            }
            else
            {
                return Vector<double>.Build.Dense(krill.Coordinates.Count);
            }
        }

        /// <summary>
        /// EQUATION 9
        /// </summary>
        private double EffectiveCoefficient(int currentIteration)
        {
            double rand = RandomGenerator.Instance.Random.NextDouble();
            double C_best = 2 * (rand + (currentIteration / MaxIteration));

            return C_best;
        }
    }
}
