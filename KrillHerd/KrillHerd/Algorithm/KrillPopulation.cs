using System.Collections.Generic;
using System.Linq;

namespace KrillHerd
{
    public class KrillPopulation
    {
        public List<Krill> Population { get; set; }
        public double AverageFitness { get { return Population.Average(x => x.Fitness); } }
        public double MedianFitness { get { return Population.Select(x => x.Fitness).ToList().Median(); } }
        public double MaximumFitness { get { return Population.Max(x => x.Fitness); } }
        public double MinimumFitness { get { return Population.Min(x => x.Fitness); } }

        public KrillPopulation(List<Krill> Population)
        {
            this.Population = Population;
        }

        /// <summary>
        /// Selects the krill with the best fitness function value
        /// </summary>
        public Krill GetBestKrill()
        {
            return Population.Where(x => x.Fitness == MaximumFitness).First();
        }

        /// <summary>
        /// A function that calculates the FITNESS for each krill in the population
        /// </summary>
        public void EvaluatePopulation(FitnessFunction fitnessFunctionDelegate)
        {
            foreach (var krill in Population)
            {
                krill.Evaluate(fitnessFunctionDelegate);
            }
        }
    }
}
