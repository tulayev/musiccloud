using Application.Core;
using Application.Repository;
using Application.Repository.IRepository;
using Application.Services;
using Application.Tracks;
using Application.Validations;
using Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(options => {
                options.UseSqlite(config.GetConnectionString("Default"));
            });
            services.AddCors(options => {
                options.AddPolicy("Cors", policy => {
                    policy.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(config["ClientUrl"]);
                });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IFileAccessor, FileAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
            services.AddSignalR();

            return services;
        }    
    }
}