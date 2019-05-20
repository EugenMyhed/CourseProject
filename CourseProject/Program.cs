using System;
using System.Diagnostics;
using System.Threading;
namespace CourseProject
{
    class Program
    {
        static Stopwatch stopwatch = new Stopwatch();
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            ErrorCalculation errors = new ErrorCalculation();
            Resolution resolution = new Resolution();

            double absoluteError;
            double relativeError;
          
            stopwatch.Start();
            resolution.gradualCalculation();
            stopwatch.Stop();
            Console.WriteLine("Gradual: " + stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            stopwatch.Start();
            resolution.calculateParallel();
            resolution.aproximateResParallel();
            stopwatch.Stop();
            Console.WriteLine("Parallel: " + stopwatch.ElapsedMilliseconds);
            absoluteError = errors.absoluteError(resolution.exactResolution, resolution.aproximateResolution);
            relativeError = errors.relativeError(resolution.exactResolution);
            Console.WriteLine("Absolute error: " + absoluteError);
            Console.WriteLine("Relative error: " + relativeError);
            Console.ReadKey();
        }
    }
}
