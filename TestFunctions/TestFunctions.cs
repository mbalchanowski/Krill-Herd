using System;

namespace KrillHerd
{
    public class TestFunctions
    {
        public static Tuple<double, double> SphereDomain = new Tuple<double, double>(-5.12, 5.12);
        public static Tuple<double, double> RastriginDomain = new Tuple<double, double>(-5.12, 5.12);
        public static Tuple<double, double> MichalewiczDomain = new Tuple<double, double>(0, Math.PI);
        public static Tuple<double, double> SchwefelDomain = new Tuple<double, double>(-500, 500);
        public static Tuple<double, double> AckleyDomain = new Tuple<double, double>(-32.768, 32.768);
        public static Tuple<double, double> GriewankDomain = new Tuple<double, double>(-600, 600);

        /// <summary>
        /// min is at f(0,0) = 0
        /// </summary>
        public static double SphereFunction(double[] coordinates)
        {
            double sum = 0;

            foreach (double x in coordinates)
            {
                sum += Math.Pow(x, 2);
            }
            return sum * -1;    // -1 since we are looking for minimum
        }

        /// <summary>
        /// min is at f(0,0) = 0
        /// </summary>
        public static double RastriginFunction(double[] coordinates)
        {
            double outcome;
            double sum = 0;
            double dimensions = coordinates.Length;

            foreach (double x in coordinates)
            {
                sum += Math.Pow(x, 2) - 10 * Math.Cos(2 * Math.PI * x);
            }

            outcome = (10 * dimensions) + sum;
            return outcome * -1;    // -1 since we are looking for minimum
        }

        /// <summary>
        /// min is at  f(2.20, 1.57) = -1.8013
        /// </summary>
        public static double MichalewiczFunction(double[] coordinates)
        {
            double outcome;
            double sum = 0;
            double m = 10;
            double dimensions = coordinates.Length;

            for (int i = 0; i < dimensions; i++)
            {
                sum += Math.Sin(coordinates[i])
                    *
                    Math.Pow(
                            Math.Sin(((i + 1) * Math.Pow(coordinates[i], 2))
                                    / Math.PI)
                            , 2 * m);
            }

            outcome = -1 * sum;
            return outcome * -1;    // -1 since we are looking for minimum
        }

        /// <summary>
        /// min is at f(420.9687,420.9687) = 0
        /// </summary>
        public static double SchwefelFunction(double[] coordinates)
        {
            double outcome;
            double sum = 0;
            double dimensions = coordinates.Length;
            outcome = 418.9829 * dimensions;

            for (int i = 0; i < dimensions; i++)
            {
                sum += coordinates[i] * (Math.Sin(Math.Sqrt(Math.Abs(coordinates[i]))));
            }

            outcome -= sum;
            return outcome * -1;    // -1 since we are looking for minimum
        }

        /// <summary>
        /// min is at f(X) = 0 at X = (0,...,0)
        /// </summary>
        public static double AckleyFunction(double[] coordinates)
        {
            double outcome;
            double a = 20;
            double b = 0.2;
            double c = 2 * Math.PI;
            double dimensions = coordinates.Length;

            double firstSum = 0;
            double secondSum = 0;

            for (int i = 0; i < dimensions; i++)
            {
                firstSum += Math.Pow(coordinates[i], 2);
            }

            for (int i = 0; i < dimensions; i++)
            {
                secondSum += Math.Cos(c * coordinates[i]);
            }

            double firstPart = Math.Exp(-b * Math.Sqrt((1 / dimensions) * firstSum));
            double secondPart = Math.Exp((1 / dimensions) * secondSum);

            outcome = -a * firstPart - secondPart + a + Math.Exp(1);
            return outcome * -1;    // -1 since we are looking for minimum
        }

        /// <summary>
        /// min is at f(X) = 0 at X = (0,...,0)
        /// </summary>
        public static double GriewankFunction(double[] coordinates)
        {
            double outcome;
            double firstSum = 0;
            double prod = 1;
            double dimensions = coordinates.Length;

            for (int i = 0; i < dimensions; i++)
            {
                firstSum += (Math.Pow(coordinates[i], 2) / 4000.0);
            }

            for (int i = 0; i < dimensions; i++)
            {
                prod *= Math.Cos(coordinates[i] / Math.Sqrt(i + 1));
            }

            outcome = (firstSum - prod) + 1;
            return outcome * -1; // -1 since we are looking for minimum
        }
    }
}
