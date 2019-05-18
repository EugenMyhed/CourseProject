using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;
namespace CourseProject
{
    class Program
    {
        static Stopwatch stopwatch = new Stopwatch();
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Resolution resolution = new Resolution();
            double absoluteError;
            double relativeError;
            Errors errors = new Errors();
            View view = new View();
            stopwatch.Start();
            resolution.gradualCalculation();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            stopwatch.Start();
            resolution.calculateParallel();
            resolution.aproximateResParallel();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            absoluteError = errors.CalculateAbsoluteError(resolution.exactResolution,
                                                        resolution.aproximateResolution,
                                                        resolution.GetxPoints,
                                                        resolution.GetTPoints);
            relativeError = errors.CalculateRelativeError(resolution.exactResolution);
            Console.WriteLine(absoluteError);
            Console.WriteLine(relativeError);
            Console.ReadKey();
        }
    }
}
