using Accounts.API.Configurations;
using Accounts.API.Custom;
using Accounts.Core.Configurations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);   

// Add services to the container.
builder.AddConfigurationRoot();

var settings = builder.GetSettings();

builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problems = new CustomProblemDetails(context);
        return new BadRequestObjectResult(problems);
    };
});

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = ApiVersion.Default;
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(p =>
{
    p.GroupNameFormat = "'v'VVV";
    p.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();

builder.AddDataBase(settings);

builder.Services.AddDependencyInjectionConfiguration();
builder.Services.AddAuthenticationConfiguration();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = settings.RedisConnection;
});

var app = builder.Build();

app.AddMigration();
app.SeedData();

app.UseSwaggerConfiguration(app.Environment);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
