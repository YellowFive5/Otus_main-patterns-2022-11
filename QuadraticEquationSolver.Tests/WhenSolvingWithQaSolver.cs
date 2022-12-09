#region Usings

using FluentAssertions;
using NUnit.Framework;

#endregion

namespace QuadraticEquationSolver.Tests
{
    public class WhenSolvingWithQaSolver : TestBase
    {
        private QaSolver Solver { get; set; }

        public override void Setup()
        {
            base.Setup();
            Solver = new QaSolver();
        }

        [TestCase(1, 0, 1)]
        public void NoRootsFound(double a, double b, double c)
        {
            var result = Solver.Solve(a, b, c);

            result.Should().BeEmpty();
        }
    }
}