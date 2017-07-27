using System.Collections.Generic;
using System.Linq;

namespace Lexicon.Base.Math.Extensions
{
    using static System.Math;

    public static class EnumerableAggregationExtensions
    {
        public static double Variance(this IEnumerable<dynamic> source)
        {
            var enumerable = source as IList<dynamic> ?? source.ToList();
            var avg = enumerable.Mean();
            var d = enumerable.Aggregate(0.0, (total, next) => total += Pow(next - avg, 2));
            return d / (enumerable.Count());
        }


        public static double Mean(this IEnumerable<dynamic> source)
        {
            var enumerable = source as IList<dynamic> ?? source.ToList();
            if (!enumerable.Any())
                return 0.0;

            double length = enumerable.Count();
            double sum = enumerable.GroupBy(x => x).Select(g => new {Value = g.Key, Count = g.Count()}).Count();
            return sum / length;
        }

        /// <summary>
        /// Normal distribution of this interger value but in our case we are are using the 
        /// group value, string value or occurance count? 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean"></param>
        /// <param name="standard_dev"></param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Normal_distribution</remarks>
        public static double NormalDist(double x, double mean, double standard_dev)
        {
            double fact = standard_dev * Sqrt(2.0 * PI);
            double expo = (x - mean) * (x - mean) / (2.0 * standard_dev * standard_dev);
            return Exp(-expo) / fact;
        }

        public static double NORMDIST(double x, double mean, double standard_dev, bool cumulative)
        {
            const double parts = 50000.0; //large enough to make the trapzoids small enough

            double lowBound = 0.0;
            if (cumulative) //do integration: trapezoidal rule used here
            {
                double width = (x - lowBound) / (parts - 1.0);
                double integral = 0.0;
                for (int i = 1; i < parts - 1; i++)
                {
                    integral += 0.5 * width * (NormalDist(lowBound + width * i, mean, standard_dev) +
                                               (NormalDist(lowBound + width * (i + 1), mean, standard_dev)));
                }
                return integral;
            }
            else //return function value
            {
                return NormalDist(x, mean, standard_dev);
            }
        }

        public static double SquareRoot(this double source)
        {
            return Sqrt(source);
        }
    }
}

