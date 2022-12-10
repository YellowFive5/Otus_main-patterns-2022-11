#region Usings

using System;

#endregion

namespace QuadraticEquationSolver
{
    public class QeSolver : ISolver
    {
        public double[] Solve(double a, double b, double c)
        {
            var d = Math.Pow(b, 2) - 4 * a * c;

            if (d < 0)
            {
                return new double[] { };
            }

            if (d > 0)
            {
                var x1 = (-b + Math.Sqrt(d)) / (2 * a);
                var x2 = (-b - Math.Sqrt(d)) / (2 * a);
                return new[] { x1, x2 };
            }

            if (d == 0)
            {
                var x = -b / (2 * a);
                return new[] { x };
            }

            return new[] { a, b, c };
        }
    }
}