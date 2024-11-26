using ProbabilityApi.Models;

namespace ProbabilityApi;

public interface ICalculationLogger
{
    void Log(double probA, double probB, ProbabilityOperation operation, double result);
}