using Microsoft.AspNetCore.Mvc;
using ProbabilityApi.Models;

namespace ProbabilityApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculateController(ILogger<CalculateController> logger, ICalculationLogger calculationLogger)
{
    [HttpPost]
    public IResult Post([FromBody] ProbabilityRequest request)
    {
        try
        {
            var calculator = new ProbabilityCalculator(calculationLogger);
            double result = calculator.Calculate(request.ProbA, request.ProbB, request.Operation);
            return Results.Ok(new { result = Math.Round(result, 4) });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            //log error
            logger.LogError("Error occured while calculating probability", ex);
            return Results.StatusCode(500);
        }
    }
}