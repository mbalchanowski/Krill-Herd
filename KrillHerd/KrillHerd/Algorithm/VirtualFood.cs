using MathNet.Numerics.LinearAlgebra;

namespace KrillHerd
{
    public class VirtualFood
    {
        private Vector<double> PositionOfFood;
        private KrillPopulation KrillPopulation;
        private Vector<double> FoodLastPosition, UB_Vector, LB_Vector;
        private double Epsilon = 0.0001;
        private FitnessFunction FitnessFunction;

        public VirtualFood(KrillPopulation krillPopulation, FitnessFunction fitnessFunction, Vector<double> UB_Vector, Vector<double> LB_Vector)
        {
            this.UB_Vector = UB_Vector;
            this.LB_Vector = LB_Vector;
            KrillPopulation = krillPopulation;
            FitnessFunction = fitnessFunction;
            PositionOfFood = Vector<double>.Build.Dense(krillPopulation.Population[0].Coordinates.Count);
        }

        /// <summary>
        /// EQUATION 12
        /// </summary>
        public Vector<double> CreateVirtualFood()
        {
            double sum = 0;

            foreach (var krill in KrillPopulation.Population)
            {
                PositionOfFood += krill.Coordinates / (krill.Fitness + Epsilon);
            }

            foreach (var krill in KrillPopulation.Population)
            {
                sum += (1 / (krill.Fitness + Epsilon));
            }

            PositionOfFood = PositionOfFood / sum;
            PositionOfFood = MathHelpers.FindLimits(PositionOfFood, KrillPopulation.GetBestKrill().Coordinates, LB_Vector, UB_Vector);

            return CompareToFoodLastPosition(PositionOfFood);
        }

        /// <summary>
        /// We compare to see if it is worth changing the position of the food
        /// </summary>
        private Vector<double> CompareToFoodLastPosition(Vector<double> centerOfFoodCoordinates)
        {
            if (FoodLastPosition != null)
            {
                if (FitnessFunction.Invoke(FoodLastPosition.ToArray()) > FitnessFunction.Invoke(centerOfFoodCoordinates.ToArray()))
                {
                    centerOfFoodCoordinates = FoodLastPosition;
                }
            }

            FoodLastPosition = centerOfFoodCoordinates;
            return centerOfFoodCoordinates;
        }
    }
}
