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
    }
}
