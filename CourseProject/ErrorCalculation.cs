using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;


namespace CourseProject
{
    class ErrorCalculation
    {
        Resolution resolution = new Resolution();
        int kMax;
        int iMax;
        double maxDifference;
        public double absoluteError(double[,] firstMatrix, double[,] secondMatrix)
        {
            for (int k = 0; k < resolution.GetTPoints; k++)
            {
                for (int i = 0; i < resolution.GetXPoints; i++)
                {
                    if (Abs(secondMatrix[k, i] - firstMatrix[k, i]) > maxDifference)
                    {
                        maxDifference = Abs(secondMatrix[k, i] - firstMatrix[k, i]);
                        kMax = k;
                        iMax = i;
                    }
                }
            }
            return maxDifference;
        }
        public double relativeError(double[,] matrix)
        {
            return maxDifference / matrix[kMax, iMax];
        }
    }
}
