#region Usings

using NUnit.Framework;

#endregion

namespace QuadraticEquationSolver.Tests
{
    public class WhenSolvingWithQaSolver : TestBase
    {
        [Test]
        public void TestPasses()
        {
            var solver = new QaSolver();

            var result = solver.Solve(1, 1, 1);

            // todo test stub
            Assert.Pass();
        }
    }
}