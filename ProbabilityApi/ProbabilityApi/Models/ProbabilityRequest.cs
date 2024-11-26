namespace ProbabilityApi.Models;

public class ProbabilityRequest
{
    public double ProbA { get; set; }
 
    public double ProbB { get; set; }

    public ProbabilityOperation Operation { get; set; }
}