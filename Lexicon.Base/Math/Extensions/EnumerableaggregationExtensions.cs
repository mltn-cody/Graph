using System.Collections.Generic;
using System.Linq;

namespace Lexicon.Base.Math.Extensions
{
    using static System.Math;

    public static class EnumerableaggregationExtensions
    {
        public static double Variance(this IEnumerable<double> source)
        {
            var enumerable = source as IList<double> ?? source.ToList();
            var avg = enumerable.Average();
            var d = enumerable.Aggregate(0.0, (total, next) => total += Pow(next - avg, 2));
            return d / (enumerable.Count() - 1);
        }

        public static double Mean(this IEnumerable<double> source)
        {
            var enumerable = source as IList<double> ?? source.ToList();
            if (!enumerable.Any())
                return 0.0;

            double length = enumerable.Count();
            double sum = enumerable.Sum();
            return sum / length;
        }

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

        public static double SquareRoot(double source)
        {
            return Sqrt(source);
        }
    }
}

