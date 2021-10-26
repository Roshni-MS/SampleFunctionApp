using System;
using System.Collections.Generic;
using System.Text;

namespace SampleSumHttpTrigger
{
    public static class Calculator
    {
        public static double AdditionOperation(double num1, double num2) { return num1 + num2; }
        public static double SubtractionOperation(double num1, double num2) { return num1 - num2; }
        public static double MultiplicationOperation(double num1, double num2) { return num1 * num2; }
        public static double DivisionOperation(double num1, double num2) { return num1 / num2; }

    }
}
