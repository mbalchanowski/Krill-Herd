using MathNet.Numerics;
using System;
using System.Collections.Generic;

namespace KrillHerd
{
    public class Neighbourhood
    {
        private readonly KrillPopulation krillPopulation;

        public Neighbourhood(KrillPopulation krillPopulation)
        {
            this.krillPopulation = krillPopulation;
        }

        /// <summary>
        /// Creates a neighborhood for a given krill
        /// </summary>
        public List<Krill> GetNeighbourhood(Krill krill)
        {
            List<Krill> neighbourhood = new List<Krill>();
            double sensingDistance = SensingDistance(krill);

            foreach (var neighbour in krillPopulation.Population)
            {
                if (neighbour.KrillNumber != krill.KrillNumber)
                {
                    // calculate the distance between one individual and the other
                    double distance = Distance.Euclidean(krill.Coordinates, neighbour.Coordinates);

                    // If the distance between two krills is less than "sensingDistance", then we add this krill to the neighborhood
                    if (distance < sensingDistance)
                    {
                        neighbourhood.Add(neighbour);
                    }
                }
            }

            return neighbourhood;
        }

        /// <summary>
        /// EQUATION 7
        /// </summary>
        private double SensingDistance(Krill krill)
        {
            double N = krillPopulation.Population.Count;

            double firstPart = (1 / ( 5 * N));
            double secondPart = 0;

            foreach (var otherKrill in krillPopulation.Population)
            {
                secondPart += Distance.Euclidean(krill.Coordinates, otherKrill.Coordinates);
            }

            double result = firstPart * secondPart;
            return Math.Round(result, 2);
        }
    }
}
