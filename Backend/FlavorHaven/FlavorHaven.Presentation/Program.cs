using FlavorHaven.Extensions;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices()
    .ConfigureJWT()
    .AddAuthentication()
    .AddSwaggerDocumentation();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();