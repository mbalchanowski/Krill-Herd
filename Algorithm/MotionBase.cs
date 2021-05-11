using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace KrillHerd
{
    public class MotionBase
    {
        protected double epsilon = 0.00001;
        protected KrillPopulation KrillPopulation;
        protected FitnessFunction FitnessFunction { get; set; }

        public MotionBase(KrillPopulation KrillPopulation, FitnessFunction fitnessFunction)
        {
            this.KrillPopulation = KrillPopulation;
            FitnessFunction = fitnessFunction;
        }

        /// <summary>
        /// EQUATION 5
        /// X_i_j = (X_j - X_i) / (|| X_j - X_i || + epsilon)
        /// </summary>
        public Vector<double> X_i_j(Vector<double> krillCoordinates, Vector<double> neihghbourCoordinates)
        {
            Vector<double> result = (neihghbourCoordinates - krillCoordinates)
                .Divide(Distance.Euclidean(neihghbourCoordinates, krillCoordinates) + epsilon);

            return result;
        }

        /// <summary>
        /// EQUATION 6
        /// K_i_j = (K_i - K_j) / (K_worst - K_best)
        /// </summary>
        public double K_i_j(double krillFitness, double neihghbourFitness)
        {
            double result = (krillFitness - neihghbourFitness) /
                (KrillPopulation.MinimumFitness - KrillPopulation.MaximumFitness);

            return result;
        }
    }
}
