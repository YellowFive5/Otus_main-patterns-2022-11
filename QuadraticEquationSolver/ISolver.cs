namespace QuadraticEquationSolver
{
    public interface ISolver
    {
        double[] Solve(double a, double b, double c, double e = 1e-5);
    }
}