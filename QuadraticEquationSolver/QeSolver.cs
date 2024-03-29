﻿#region Usings

using System;

#endregion

namespace QuadraticEquationSolver
{
    public class QeSolver : ISolver
    {
        public double[] Solve(double a, double b, double c, double e = 1e-5)
        {
            if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c) ||
                double.IsInfinity(a) || double.IsInfinity(b) || double.IsInfinity(c))
            {
                throw new Exception("Wrong coefficient value");
            }

            if (Math.Abs(a) < e)
            {
                throw new Exception("'A' must not be zero");
            }

            var d = Math.Pow(b, 2) - 4 * a * c;

            if (Math.Abs(d) < e)
            {
                var x = -b / (2 * a);
                return new[] { x };
            }

            if (d > e)
            {
                var x1 = (-b + Math.Sqrt(d)) / (2 * a);
                var x2 = (-b - Math.Sqrt(d)) / (2 * a);
                return new[] { x1, x2 };
            }

            return new double[] { };
        }
    }
}