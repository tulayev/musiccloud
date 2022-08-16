using API.Extensions;
using API.Middleware;
using Application.Tracks;
using FluentValidation.AspNetCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation(config => {
    config.RegisterValidatorsFromAssemblyContaining<TrackValidator>();
});

builder.Services.AddApplicationServices(builder.Configuration);

var app = await builder.Build().MigrateDatabaseAsync<DataContext>();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();