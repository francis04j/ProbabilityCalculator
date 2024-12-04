global using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProbabilityApi;
//using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using ProbabilityApi.Extensions;
using ProbabilityApi.Middleware;
using ProbabilityApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<ICalculationLogger>(new FileCalculationLogger("calculation_logs.txt"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck("SampleCheck", () =>
        HealthCheckResult.Healthy("The app is running fine."));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProbabilityRequestValidator>();

builder.Services.AddProblemDetails(options =>
{

// Custom mapping function for FluentValidation's ValidationException.
    options.MapFluentValidationException();

// You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
// This is useful if you have upstream middleware that needs to do additional handling of exceptions.
    options.Rethrow<NotSupportedException>();

// This will map NotImplementedException to the 501 Not Implemented status code.
    options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

// This will map HttpRequestException to the 503 Service Unavailable status code.
    options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

// Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
// If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

ProblemDetailsExtensions.UseProblemDetails(app);
app.UseMiddleware<ValidationExceptionMiddleware>();

app.Run();