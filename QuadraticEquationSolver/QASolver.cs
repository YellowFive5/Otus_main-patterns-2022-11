#region Usings

using System;

#endregion

namespace QuadraticEquationSolver
{
    public class QaSolver : ISolver
    {
        public double[] Solve(double a, double b, double c)
        {
            var d = Math.Pow(b, 2) - 4 * a * c;

            if (d < 0)
            {
                return new double[] { };
            }

            return new[] { a, b, c };
        }
    }
}