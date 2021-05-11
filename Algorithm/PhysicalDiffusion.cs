using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;

namespace KrillHerd
{
    public class PhysicalDiffusion
    {
        private double D_max;
        private int Iterations, Dimensions;

        public PhysicalDiffusion(double D_max, int iterations, int dimensions)
        {
            Iterations = iterations;
            this.D_max = D_max;
            Dimensions = dimensions;
        }

        /// <summary>
        /// Equation 17
        /// </summary>
        public Vector<double> GetPhysicalDiffusion(int i)
        {
            var randomVector = Vector<double>.Build.Random(Dimensions, new ContinuousUniform(-1, 1));
            double diffusion = D_max * (1 - ((double)i / Iterations));
            var D_i = randomVector.Multiply(diffusion);

            return D_i;
        }
    }
}