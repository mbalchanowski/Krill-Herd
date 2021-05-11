using System.Collections.Generic;
using System.Linq;

namespace KrillHerd
{
    public static class Extensions
    {
        public static double Median(this List<double> source)
        {
            var sortedList = from number in source
                             orderby number
                             select number;

            int count = sortedList.Count();
            int itemIndex = count / 2;
            if (count % 2 == 0) // Even number of items. 
                return (sortedList.ElementAt(itemIndex) +
                        sortedList.ElementAt(itemIndex - 1)) / 2;

            // Odd number of items. 
            return sortedList.ElementAt(itemIndex);
        }
    }
}
