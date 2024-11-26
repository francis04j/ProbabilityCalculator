using Microsoft.AspNetCore.Mvc;
using ProbabilityApi.Models;

namespace ProbabilityApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculateController
{
    private readonly ILogger<CalculateController> _logger;

    public CalculateController(ILogger<CalculateController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IResult Post([FromBody] ProbabilityRequest request)
    {
        try
        {
            var calculator = new ProbabilityCalculator();
            double result = calculator.Calculate(request.ProbA, request.ProbB, request.Operation);
            return Results.Ok(new { result = Math.Round(result, 4) });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            //log error
            return Results.StatusCode(500);
        }
    }
}