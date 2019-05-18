using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseProject
{
    class Resolution
    {
        View view = new View();
        Calculation calc = new Calculation();
        List<Task> tasksArray = new List<Task>();

        int threadCount = 4; 
        int taskNumber = 0;
        double x = 0;
        double t = 0;
        const int pointsX = 15;
        const int pointsT = 5000;
        double stepX = 1d / pointsX;
        double stepT = 1d / pointsT;
        public double[,] exactResolution = new double[pointsT, pointsX];
        public double[,] aproximateResolution = new double[pointsT, pointsX];
        public double[,] exactResolutionParallel = new double[pointsT, pointsX];
        public double[,] aproximateResolutionParallel = new double[pointsT, pointsX];

        public int GetxPoints
        {
            get { return pointsX; }
        }
        public int GetTPoints
        {
            get { return pointsT; }
        }

        public void gradualCalculation()
        {
   
            t = 0d;
            for (int i = 0; i < pointsT; i++)
            {
                x = 0d;
                for (int j = 0; j < pointsX; j++)
                {
                    exactResolution[i, j] = calc.exactResolution(x, t);
                    x += stepX;
                }
                t += stepT;
            }

            x = 0d;
            for (int i = 0; i < pointsX; i++)
            {
                aproximateResolution[0, i] = calc.exactResolution(x, 0);
                x += stepX;
            }
            t = 0d;
            for (int i = 0; i < pointsT; i++)
            {
                aproximateResolution[i, 0] = calc.exactResolution(0, t);
                aproximateResolution[i, pointsX - 1] = calc.exactResolution(1, t);
                t += stepT;
            }


            for (int i = 1; i < pointsT; i++)
            {
                for (int j = 1; j < pointsX - 1; j++)
                {
                    aproximateResolution[i, j] = calc.aproximateResolution(
                        aproximateResolution[i - 1, j - 1],
                        aproximateResolution[i - 1, j + 1],
                        aproximateResolution[i - 1, j],
                        stepX,
                        stepT
                    );
                    //if (aproximateResolution[i, j] != exactResolution[i, j])
                    //    Console.WriteLine(aproximateResolution[i, j] + " != " + exactResolution[i, j]);

                }

            }
            Console.WriteLine();
            view.Writer("C:/Users/Eugene/Desktop/gradual_1.txt", aproximateResolution);
            view.Writer("C:/Users/Eugene/Desktop/gradual_2.txt", exactResolution);
        }

        public async void calculateParallel()
        {
            for (int i = 0; i < threadCount; i++)
            {
                tasksArray.Add(Task.Factory.StartNew(delegate () { exactResParallel(taskNumber++); }));
            }
            await Task.WhenAll(tasksArray);
            Parallel.For(0, pointsX, (int i) =>
            {
                double xLocal = stepX * i;
                aproximateResolutionParallel[0, i] = calc.exactResolution(xLocal, 0);
            });
            Parallel.For(0, pointsT, (int i) =>
            {
                double tLocal = i * stepT;
                aproximateResolutionParallel[i, 0] = calc.exactResolution(0, tLocal);
                aproximateResolutionParallel[i, pointsX - 1] = calc.exactResolution(1, tLocal);
            });
            aproximateResParallel();

            view.Writer("C:/Users/Eugene/Desktop/parallel_1.txt", aproximateResolutionParallel);
            view.Writer("C:/Users/Eugene/Desktop/parallel_2.txt", exactResolutionParallel);
        }

        public void exactResParallel(int taskNumber)
        {
            int startIndex = taskNumber * (pointsT / threadCount);
            int endIndex;
            if (taskNumber == threadCount - 1)
            {
                endIndex = pointsT;
            }
            else
            {
                endIndex = startIndex + pointsT / threadCount;
            }
            double tLocal = (double)startIndex / pointsT;
            double xLocal;
            for (int k = startIndex; k < endIndex; k++)
            {
                xLocal = 0d;
                for (int i = 0; i < pointsX; i++)
                {
                    Interlocked.Exchange(ref exactResolutionParallel[k, i], calc.exactResolution(xLocal, tLocal));
                    xLocal += stepX;
                }
                tLocal += stepT;
            }
        }

        public void aproximateResParallel()
        {
            for (int i = 1; i < pointsT; i++)
            {
                Parallel.For(1, pointsX - 1, (int j) =>
                {
                    aproximateResolutionParallel[i, j] = calc.aproximateResolution(
                        aproximateResolutionParallel[i - 1, j - 1],
                        aproximateResolutionParallel[i - 1, j + 1],
                        aproximateResolutionParallel[i - 1, j],
                        stepX,
                        stepT
                    );
                    //if (aproximateResolutionParallel[i, j] != aproximateResolution[i, j] )
                    //    Console.WriteLine(aproximateResolutionParallel[i, j]+": " + aproximateResolution[i, j]);
                });
            }
        }
    }
}


