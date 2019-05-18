using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class Calculation
    {
        public static double a = 1.7;
        public static double C = 32;


        public double exactResolution(double x, double t)
        {
            return (Math.Pow((C - 4 * a * t), 3 / 2d) * Math.Pow((Math.Pow((C - 4 * a * t), 3 / 2d) - Math.Pow(x, 2)), -3 / 2d));
        }

        public double aproximateResolution(double wiPrev, double wiNext, double wi, double h, double tau)
        {
            double t1 = -2.0 / 3d * Math.Pow(wi, -5.0 / 3d);
            double t2 = Math.Pow((wiNext - wiPrev) / (2d * h), 2);
            double t3 = Math.Pow(wi, -2 / 3d);
            double t4 = (wiPrev - 2d * wi + wiNext) / Math.Pow(h, 2);

            return wi + tau * (t1 * t2 + t3 * t4);
        }
    }
}
