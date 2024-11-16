using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
string? dataBaseConnection = builder.Configuration.GetConnectionString("PostrgeSql");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(dataBaseConnection));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();