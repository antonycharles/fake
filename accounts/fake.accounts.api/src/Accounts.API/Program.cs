using Accounts.API.Configurations;
using Accounts.Core.Configurations;

var builder = WebApplication.CreateBuilder(args);   

// Add services to the container.
builder.AddConfigurationRoot();

var settings = builder.GetSettings();

builder.Services.AddControllers();

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
