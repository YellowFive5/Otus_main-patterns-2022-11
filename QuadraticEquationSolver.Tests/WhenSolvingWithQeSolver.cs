#region Usings

using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

#endregion

namespace QuadraticEquationSolver.Tests
{
    public class WhenSolvingWithQeSolver : TestBase
    {
        private QeSolver Solver { get; set; }

        public override void Setup()
        {
            base.Setup();
            Solver = new QeSolver();
        }

        [TestCase(1, 0, 1)]
        public void NoRootsFound(double a, double b, double c)
        {
            var result = Solver.Solve(a, b, c);

            result.Should().BeEmpty();
        }

        [TestCase(1, 0, -1, 1, -1)]
        public void TwoRootsFound(double a, double b, double c, double x1Expected, double x2Expected)
        {
            var result = Solver.Solve(a, b, c);

            result.Length.Should().Be(2);
            result.ElementAt(0).Should().Be(x1Expected);
            result.ElementAt(1).Should().Be(x2Expected);
        }

        [TestCase(1, 2, 1, -1)]
        public void OneRootFound(double a, double b, double c, double xExpected)
        {
            var result = Solver.Solve(a, b, c);

            result.Length.Should().Be(1);
            result.First().Should().Be(xExpected);
        }

        [TestCase(0, 1, 1)]
        public void ExceptionThrowsWhenAEqualsZero(double a, double b, double c)
        {
            Action act = () => Solver.Solve(a, b, c);

            act.Should()
               .Throw<Exception>()
               .WithMessage("'A' must not be zero");
        }
    }
}