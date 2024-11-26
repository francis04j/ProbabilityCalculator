using System;
using Xunit;
using FluentAssertions;
using ProbabilityApi;
using ProbabilityApi.Models;

namespace UnitTests;

public class ProbabilityCalculatorTests
{
    private readonly ProbabilityCalculator _calculator;

    public ProbabilityCalculatorTests()
    {
        _calculator = new ProbabilityCalculator();
    }

    [Theory]
    [InlineData(0.5, 0.4, ProbabilityOperation.CombinedWith, 0.2)]
    [InlineData(0.7, 0.5, ProbabilityOperation.Either, 0.85)]
    [InlineData(0.1, 0.9, ProbabilityOperation.CombinedWith, 0.09)]
    [InlineData(0.3, 0.3, ProbabilityOperation.Either, 0.51)]
    public void Calculate_ValidInputs_ReturnsExpectedResults(
        double probA, double probB, ProbabilityOperation operation, double expected)
    {
        // Act
        double result = _calculator.Calculate(probA, probB, operation);

        // Assert
        result.Should().BeApproximately(expected, 0.0001);
    }

    [Theory]
    [InlineData(-0.1, 0.5, ProbabilityOperation.CombinedWith)]
    [InlineData(1.2, 0.4, ProbabilityOperation.Either)]
    [InlineData(0.6, -0.2, ProbabilityOperation.CombinedWith)]
    [InlineData(0.6, 1.5, ProbabilityOperation.Either)]
    public void Calculate_InvalidProbabilities_ThrowsArgumentException(
        double probA, double probB, ProbabilityOperation operation)
    {
        // Act
        Action act = () => _calculator.Calculate(probA, probB, operation);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid probability values. Probabilities must be between 0 and 1.");
    }

    [Fact]
    public void Calculate_InvalidOperation_ThrowsInvalidOperationException()
    {
        // Act
        Action act = () => _calculator.Calculate(0.5, 0.4, (ProbabilityOperation)999);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Invalid operation");
    }
}
