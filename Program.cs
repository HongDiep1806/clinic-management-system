using Microsoft.EntityFrameworkCore;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using ClinicManagementSystem.Mappings;
using MediatR;
using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.Services;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;


namespace ClinicManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //        var builder = WebApplication.CreateBuilder(args);

            //        // Add services to the container.
            //        builder.Services.AddControllers();
            //        builder.Services.AddEndpointsApiExplorer();
            //        builder.Services.AddSwaggerGen();

            //        builder.Services.AddCors(options =>
            //        {
            //            options.AddPolicy("AllowVueApp",
            //                policy =>
            //                {
            //                    policy.WithOrigins("http://localhost:5173") // địa chỉ của frontend Vue
            //                          .AllowAnyHeader()
            //                          .AllowAnyMethod()
            //                          .AllowCredentials();
            //                });
            //        });

            //        // Add DbContext
            //        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //        builder.Services.AddDbContext<RestoreDbContext>(options =>
            //            options.UseSqlServer(builder.Configuration.GetConnectionString("RestoreConnection")));

            //        // Add Identity and AutoMapper
            //        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            //        builder.Services.AddAutoMapper(typeof(Program));
            //        builder.Services.AddAutoMapper(typeof(UserProfile));

            //        // Add MediatR
            //        builder.Services.AddMediatR(typeof(Program));
            //        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            //        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ClinicManagementSystem.Pipeline.ValidationPipelineBehavior<,>));


            //        // Register custom services
            //        builder.Services.AddScoped<IUserService, UserService>();
            //        builder.Services.AddScoped<IUserRepository, UserRepository>();
            //        builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            //        builder.Services.AddScoped<IRoleService, RoleService>();
            //        builder.Services.AddScoped<IJwtService, JwtService>();
            //        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            //        builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            //        builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            //        builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            //        builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            //        builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            //        builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            //        builder.Services.AddScoped<IMedicineService, MedicineService>();
            //        builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
            //        builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            //        builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
            //        builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            //        builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            //        builder.Services.AddScoped<IScheduleService, ScheduleService>();
            //        builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
            //        builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //        builder.Services.AddScoped<IDepartmentService, DepartmentService>();


            //        builder.Services.AddHttpContextAccessor();

            //        //
            //        builder.Services.AddSwaggerGen(options =>
            //        {
            //            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            //            {
            //                Title = "Clinic Management API",
            //                Version = "v1"
            //            });

            //            // Bổ sung cấu hình bảo mật JWT
            //            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //            {
            //                Name = "Authorization",
            //                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            //                Scheme = "Bearer",
            //                BearerFormat = "JWT",
            //                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            //                Description = "Nhập JWT token vào đây (không cần từ 'Bearer')"
            //            });

            //            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            //{
            //    {
            //        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //        {
            //            Reference = new Microsoft.OpenApi.Models.OpenApiReference
            //            {
            //                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            //                Id = "Bearer"
            //            }
            //        },
            //        Array.Empty<string>()
            //    }
            //});
            //        });

            //        builder.Services.AddAuthentication("Bearer")
            //                        .AddJwtBearer("Bearer", options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            //        ValidAudience = builder.Configuration["JwtSettings:Audience"],
            //        ValidateIssuerSigningKey = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
            //    };
            //    options.Events = new JwtBearerEvents
            //    {
            //        OnChallenge = context =>
            //        {
            //            context.HandleResponse();
            //            context.Response.StatusCode = 401;
            //            context.Response.ContentType = "application/json";
            //            return context.Response.WriteAsync("{\"message\": \"Unauthorized\"}");
            //        },
            //        OnForbidden = context =>
            //        {
            //            context.Response.StatusCode = 403;
            //            context.Response.ContentType = "application/json";
            //            return context.Response.WriteAsync("{\"message\": \"Forbidden: Insufficient role\"}");
            //        }
            //    };
            //    options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;

            //});




            //        var app = builder.Build();
            //        app.UseCors("AllowVueApp");


            //        if (app.Environment.IsDevelopment())
            //        {
            //            app.UseSwagger();
            //            app.UseSwaggerUI();
            //        }
            //        //middleware
            //        app.UseMiddleware<ClinicManagementSystem.Middleware.ExceptionHandlingMiddleware>();

            //        app.UseHttpsRedirection();
            //        app.UseAuthentication();
            //        app.UseAuthorization();

            //        app.MapControllers();

            //        app.Run();
            var builder = WebApplication.CreateBuilder(args);

            // =======================================================
            // 1) CORS CHUẨN CHO COOKIE (KHÔNG DÙNG ORIGIN WILDCARD)
            // =======================================================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp", policy =>
                {
                    policy
                        .WithOrigins(
                            "https://clinicweb-production-e031.up.railway.app",
                            "http://localhost:5173"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();  // Bắt buộc cho cookie
                });
            });

            // =======================================================
            // 2) Controllers + Swagger
            // =======================================================
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Clinic Management API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Nhập token không cần chữ Bearer"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // =======================================================
            // 3) DB — SQL Server
            // =======================================================
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<RestoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RestoreConnection")));

            // =======================================================
            // 4) Dependency Injection
            // =======================================================
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(UserProfile));

            builder.Services.AddMediatR(typeof(Program));
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ClinicManagementSystem.Pipeline.ValidationPipelineBehavior<,>));

            // Custom services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IMedicineService, MedicineService>();
            builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
            builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddHttpContextAccessor();

            // =======================================================
            // 5) JWT Authentication – refresh token FROM COOKIE
            // =======================================================
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            // Cho phép /refresh không cần Authorization header
                            if (context.HttpContext.Request.Path.StartsWithSegments("/api/Auth/refresh"))
                                return Task.CompletedTask;

                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            // =======================================================
            // 6) Swagger
            // =======================================================
            app.UseSwagger();
            app.UseSwaggerUI();

            // =======================================================
            // 7) MIDDLEWARE ORDER — CỰC QUAN TRỌNG
            // =======================================================
            app.UseRouting();

            app.UseCors("AllowVueApp");     // ⭐ ĐÚNG VỊ TRÍ ⭐

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            // Railway port binding
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Clear();
            app.Urls.Add($"http://0.0.0.0:{port}");

            app.MapControllers();

            app.Run();


        }
    }
}
