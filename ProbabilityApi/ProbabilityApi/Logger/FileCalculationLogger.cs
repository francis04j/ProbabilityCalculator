using ProbabilityApi.Models;

namespace ProbabilityApi;

using System;
using System.IO;

public class FileCalculationLogger : ICalculationLogger
{
    private readonly string _logFilePath;

    public FileCalculationLogger(string logFilePath = "calculation_logs.txt")
    {
        _logFilePath = logFilePath;
    }

    public void Log(double probA, double probB, ProbabilityOperation operation, double result)
    {
        string logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | Operation: {operation} | ProbA: {probA} | ProbB: {probB} | Result: {Math.Round(result, 4)}";

        try
        {
            File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log calculation: {ex.Message}");
        }
    }
}
