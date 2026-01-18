using API.Extensions;
using API.Middleware;
using Application;
using Application.Hubs;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => 
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddAppServices(builder.Configuration);


var app = await builder.Build().MigrateDatabaseAsync();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("Cors");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
});

app.Run();
