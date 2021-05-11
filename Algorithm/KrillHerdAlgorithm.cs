using MathNet.Numerics.LinearAlgebra;
using System;

namespace KrillHerd
{
    public class KrillHerdAlgorithm
    {
        public event EventHandler<RunEventArgs> OnGenerationComplete;
        public event EventHandler<RunEventArgs> OnRunComplete;
        private KrillPopulation KrillPopulation { get; set; }
        private FitnessFunction FitnessFunction { get; set; }
        private Vector<double> LB_vector, UB_vector;

        public KrillHerdAlgorithm(KrillPopulation krillPopulation, FitnessFunction fitnessFunctionDelegate, Vector<double> LB_vector, Vector<double> UB_vector)
        {
            this.LB_vector = LB_vector;
            this.UB_vector = UB_vector;
            KrillPopulation = krillPopulation;
            FitnessFunction = fitnessFunctionDelegate;
        }

        public void Run(int evaluations, double C_t, double N_max, double V_f, double D_max)
        {
            var scaleVector = CalculateScaleVector(C_t, UB_vector, LB_vector);
            InducedMotion inducedMotion = new InducedMotion(KrillPopulation, FitnessFunction, evaluations, N_max);
            ForagingMotion foragingMotion = new ForagingMotion(KrillPopulation, FitnessFunction, evaluations, V_f);
            VirtualFood virtualFood = new VirtualFood(KrillPopulation, FitnessFunction, UB_vector, LB_vector);
            PhysicalDiffusion physicalDiffusion = new PhysicalDiffusion(D_max, evaluations, scaleVector.Count);

            // We evaluate each krill in the population
            KrillPopulation.EvaluatePopulation(FitnessFunction);
            
            for (int i = 1; i <= evaluations; i++)
            {
                var vf_position = virtualFood.CreateVirtualFood();

                foreach (var krill in KrillPopulation.Population)
                {
                    Vector<double> N_i = inducedMotion.GetInducedMotion(krill, i);
                    Vector<double> F_i = foragingMotion.GetForagingMotion(krill, i, vf_position);
                    Vector<double> D_i = physicalDiffusion.GetPhysicalDiffusion(i);

                    Vector<double> X_i = (F_i + N_i).Add(D_i); // EQUATION 1
                    X_i = X_i.PointwiseMultiply(scaleVector); 
                    var newPosition = krill.Coordinates + X_i;

                    var bestKrillPosition = KrillPopulation.GetBestKrill().Coordinates;
                    krill.Coordinates = MathHelpers.FindLimits(newPosition, bestKrillPosition, LB_vector, UB_vector);
                }

                // We evaluate each krill in the population
                KrillPopulation.EvaluatePopulation(FitnessFunction);

                OnGenerationComplete.Invoke(KrillPopulation, new RunEventArgs(i));
            }

            OnRunComplete.Invoke(KrillPopulation, new RunEventArgs(evaluations));
        }

        /// <summary>
        /// EQUATION 19
        /// </summary>
        private Vector<double> CalculateScaleVector(double C_t, Vector<double> UB_vector, Vector<double> LB_vector)
        {
            return C_t * (UB_vector - LB_vector);
        }
    }
}
