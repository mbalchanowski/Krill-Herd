using MathNet.Numerics.LinearAlgebra;
using System.Linq;

namespace KrillHerd
{
    public static class MathHelpers
    {
        /// <summary>
        /// A function that prevents krill and food from going out of the domain
        /// 
        /// Boundary Constraint Handling Scheme used in this code:
        /// Gandomi A.H., Yang X.S., "Evolutionary Boundary Constraint Handling Scheme"
        /// Neural Computing & Applications, 2012, 21(6):1449-1462.
        /// DOI: 10.1007/s00521-012-1069-0
        /// </summary>
        public static Vector<double> FindLimits(Vector<double> coordinates, Vector<double> globalBest, Vector<double> LB_Vector, Vector<double> UB_Vector)
        {
            var n = coordinates.Count;
            var ns_temp = coordinates;

            for (int i = 0; i < n; i++)
            {
                var I = ns_temp.Zip(LB_Vector, (a, b) => a < b).ToList();
                var J = ns_temp.Zip(UB_Vector, (a, b) => a > b).ToList();

                var A = RandomGenerator.Instance.Random.NextDouble();

                foreach (var item in I)
                {
                    if (item)
                    {
                        ns_temp[i] = A * LB_Vector[i] + (1 - A) * globalBest[i];
                    }
                }

                var B = RandomGenerator.Instance.Random.NextDouble();

                foreach (var item in J)
                {
                    if (item)
                    {
                        ns_temp[i] = B * UB_Vector[i] + (1 - B) * globalBest[i];
                    }
                }
            }
            return ns_temp;
        }
    }
}
