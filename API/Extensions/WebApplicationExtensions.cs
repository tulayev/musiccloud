using System.Text;
using System.Text.Json;
using Data;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<DataContext>();
                    await db.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return app;
        }

        // public static void UseFluentValidationExceptionHandler(this WebApplication app)
        // {
        //     app.UseExceptionHandler(x => 
        //     {
        //         x.Run(async context => 
        //         {
        //             var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        //             var exception = errorFeature.Error;

        //             if (!(exception is ValidationException validationException))
        //             {
        //                 throw exception;
        //             }

        //             var errors = validationException.Errors.Select(error => new
        //             {
        //                 error.PropertyName,
        //                 error.ErrorMessage
        //             });

        //             var errorText = JsonSerializer.Serialize(errors);
        //             context.Response.StatusCode = 400;
        //             context.Response.ContentType = "application/json";
        //             await context.Response.WriteAsync(errorText, Encoding.UTF8);
        //         });
        //     });
        // }
    }
}