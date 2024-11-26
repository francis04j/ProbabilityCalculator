using ProbabilityApi.Models;

namespace ProbabilityApi;

using System;

public class ProbabilityCalculator
{
    public double Calculate(double probA, double probB, ProbabilityOperation operation)
    {
        // Validate inputs
        if (probA < 0 || probA > 1 || probB < 0 || probB > 1)
        {
            throw new ArgumentException("Invalid probability values. Probabilities must be between 0 and 1.");
        }

        // Perform calculation based on the operation
        return operation switch
        {
            ProbabilityOperation.CombinedWith => probA * probB,
            ProbabilityOperation.Either => probA + probB - (probA * probB),
            _ => throw new InvalidOperationException("Invalid operation")
        };
    }
}
