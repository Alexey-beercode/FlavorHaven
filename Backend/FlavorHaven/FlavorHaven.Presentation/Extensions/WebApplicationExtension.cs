using FlavorHaven.Middlewares;
using Microsoft.AspNetCore.HttpOverrides;

namespace FlavorHaven.Extensions;

public static class WebApplicationExtension
{
    public static WebApplication AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
    
    public static WebApplication AddApplicationMiddleware(this WebApplication app)
    {
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        
        app.UseForwardedHeaders(new ForwardedHeadersOptions {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        
        app.UseHttpsRedirection();
        
        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        return app;
    }
}