using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class MathHelpers
    {
        public static long GreatestCommonFactor(long firstNumber, long secondNumber)
        {
            while (secondNumber != 0)
            {
                long temp = secondNumber;
                secondNumber = firstNumber % secondNumber;
                firstNumber = temp;
            }
            return firstNumber;
        }

        public static long LeastCommonMultiple(long firstNumber, long secondNumber)
        {
            return (firstNumber / GreatestCommonFactor(firstNumber, secondNumber)) * secondNumber;
        }

        //ChatGPT came up with this no cap
        public static double[] SolveSystemOfEquations(double[] x, double[] y)
        {
            if (x.Length != 3 || y.Length != 3)
            {
                throw new ArgumentException("The system of equations must have exactly three equations.");
            }

            double[,] coefficients = {
                { x[0] * x[0], x[0], 1 },
                { x[1] * x[1], x[1], 1 },
                { x[2] * x[2], x[2], 1 }
            };

            double[] constants = [y[0], y[1], y[2]];

            for (int i = 0; i < 3; i++)
            {
                double pivot = coefficients[i, i];

                for (int j = 0; j < 3; j++)
                {
                    coefficients[i, j] /= pivot;
                }

                constants[i] /= pivot;

                for (int k = 0; k < 3; k++)
                {
                    if (k != i)
                    {
                        double factor = coefficients[k, i];

                        for (int j = 0; j < 3; j++)
                        {
                            coefficients[k, j] -= factor * coefficients[i, j];
                        }

                        constants[k] -= factor * constants[i];
                    }
                }
            }

            return [constants[0], constants[1], constants[2]];
        }

        public static long[] SolveSystemOfEquations(long[] x, long[] y)
        {
            if (x.Length != 3 || y.Length != 3)
            {
                throw new ArgumentException("The system of equations must have exactly three equations.");
            }

            double[,] coefficients = {
                { x[0] * x[0], x[0], 1 },
                { x[1] * x[1], x[1], 1 },
                { x[2] * x[2], x[2], 1 }
            };

            double[] constants = [y[0], y[1], y[2]];

            for (int i = 0; i < 3; i++)
            {
                double pivot = coefficients[i, i];

                for (int j = 0; j < 3; j++)
                {
                    coefficients[i, j] /= pivot;
                }

                constants[i] /= pivot;

                for (int k = 0; k < 3; k++)
                {
                    if (k != i)
                    {
                        double factor = coefficients[k, i];

                        for (int j = 0; j < 3; j++)
                        {
                            coefficients[k, j] -= factor * coefficients[i, j];
                        }

                        constants[k] -= factor * constants[i];
                    }
                }
            }

            return [(long)constants[0], (long)constants[1], (long)constants[2]];
        }
    }
}
