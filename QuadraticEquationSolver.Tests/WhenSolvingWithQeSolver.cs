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

        [TestCase(1, 2, 1.0000000001, -1)]
        public void OneRootFound(double a, double b, double c, double xExpected)
        {
            var result = Solver.Solve(a, b, c);

            result.Length.Should().Be(1);
            result.First().Should().Be(xExpected);
        }

        [TestCase(0.0000000001)]
        public void ExceptionThrowsWhenAEqualsZero(double a)
        {
            Action act = () => Solver.Solve(a, 0, 0);

            act.Should()
               .Throw<Exception>()
               .WithMessage("'A' must not be zero");
        }

        [TestCase(double.NaN, 0, 0)]
        [TestCase(double.NegativeInfinity, 0, 0)]
        [TestCase(double.PositiveInfinity, 0, 0)]
        [TestCase(0, double.NaN, 0)]
        [TestCase(0, double.NegativeInfinity, 0)]
        [TestCase(0, double.PositiveInfinity, 0)]
        [TestCase(0, 0, double.NaN)]
        [TestCase(0, 0, double.NegativeInfinity)]
        [TestCase(0, 0, double.PositiveInfinity)]
        public void ExceptionThrowsWhenCoefficientNotANumber(double a, double b, double c)
        {
            Action act = () => Solver.Solve(a, b, c);

            act.Should()
               .Throw<Exception>()
               .WithMessage("Wrong coefficient value");
        }
    }
}