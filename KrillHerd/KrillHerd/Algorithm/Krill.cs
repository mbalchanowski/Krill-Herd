using MathNet.Numerics.LinearAlgebra;

namespace KrillHerd
{
    public class Krill
    {
        public double Fitness { get; set; }
        public double BestFitness { get; set; }
        public Vector<double> BestCoordinates { get; set; }
        public Vector<double> Coordinates;
        public int KrillNumber = 0;

        public Krill(Vector<double> Coordinates)
        {
            this.Coordinates = Coordinates;
        }

        public Krill(int krillNumber)
        {
            KrillNumber = krillNumber;
            BestFitness = double.MinValue;
        }

        /// <summary>
        /// Calculation of fitness for a given krill
        /// </summary>
        public double Evaluate(FitnessFunction fitnessFunction)
        {
            Fitness =  fitnessFunction.Invoke(Coordinates.ToArray());

            if (Fitness > BestFitness)
            {
                BestFitness = Fitness;
                BestCoordinates = Coordinates;
            }

            return Fitness;
        }
    }
}
