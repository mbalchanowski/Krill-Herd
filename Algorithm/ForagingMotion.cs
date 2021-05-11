using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace KrillHerd
{
    public class ForagingMotion : MotionBase
    {
        private double V_f;
        private int MaxIteration;
        private Vector<double> F_i_old;
        private Dictionary<Tuple<int, int>, Vector<double>> ForagingSpeedHistory;

        public ForagingMotion(KrillPopulation KrillPopulation, FitnessFunction fitnessFunction, int maxIteration, double V_f) 
            : base(KrillPopulation, fitnessFunction)
        {
            F_i_old = Vector<double>.Build.Dense(KrillPopulation.Population[0].Coordinates.Count);
            this.V_f = V_f;
            MaxIteration = maxIteration;
            ForagingSpeedHistory = new Dictionary<Tuple<int, int>, Vector<double>>();
        }

        /// <summary>
        /// EQUATION 10
        /// </summary>
        public Vector<double> GetForagingMotion(Krill krill, int currentIteration, Vector<double> vf_position)
        {
            int lastIteration = currentIteration - 1;
            Vector<double> F_i_old = ForagingSpeedHistory.ContainsKey(new Tuple<int, int>(lastIteration, krill.KrillNumber))
                ? ForagingSpeedHistory[new Tuple<int, int>(lastIteration, krill.KrillNumber)]
                : Vector<double>.Build.Dense(krill.Coordinates.Count);

            double Omega_f = (0.1 + (0.8 * (1 - currentIteration / MaxIteration)));
            Vector<double> B_i = (Beta_i_best(krill) + Beta_i_food(krill, lastIteration, vf_position)); // EQUATION 11
            Vector<double> F_i = B_i.Multiply(V_f) + F_i_old.Multiply(Omega_f); // EQUATION 10

            ForagingSpeedHistory.Add(new Tuple<int, int>(currentIteration, krill.KrillNumber), F_i);

            return F_i;
        }

        /// <summary>
        /// EQUATION 13
        /// </summary>
        private Vector<double> Beta_i_food(Krill krill, int currentIteration, Vector<double> vf_position)
        {
            Vector<double> result = Vector<double>.Build.Dense(krill.Coordinates.Count);

            var K_food = FitnessFunction.Invoke(vf_position.ToArray());
            if (K_food > krill.Fitness)
            {
                double C_food = EffectiveFoodCoefficient(currentIteration);
                double K_i_food = K_i_j(krill.Fitness, K_food);
                Vector<double> X_i_food = X_i_j(krill.Coordinates, vf_position);

                result = X_i_food.Multiply(K_i_food * C_food);
            }

            return result;
        }

        /// <summary>
        /// EQUATION 14
        /// </summary>
        private double EffectiveFoodCoefficient(int currentIteration)
        {
            return 2 * (1 - (currentIteration / MaxIteration));
        }

        /// <summary>
        /// EQUATION 15
        /// </summary>
        /// <returns></returns>
        private Vector<double> Beta_i_best(Krill krill)
        {
            if (krill.BestFitness > krill.Fitness)
            {
                return X_i_j(krill.Coordinates, krill.BestCoordinates).Multiply(K_i_j(krill.Fitness, krill.BestFitness));
            }
            else
            {
                return Vector<double>.Build.Dense(krill.Coordinates.Count);
            }
        }
    }
}