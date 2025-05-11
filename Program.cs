using Microsoft.EntityFrameworkCore;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using ClinicManagementSystem.Mappings;
using MediatR;
using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.Services;

namespace ClinicManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<RestoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RestoreConnection")));

            // Add Identity and AutoMapper
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(UserProfile));

            // Add MediatR
            builder.Services.AddMediatR(typeof(Program));

            // Register custom services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
