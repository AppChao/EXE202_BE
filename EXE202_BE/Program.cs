using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using EXE202_BE.Service;
using EXE202_BE.Repository;
using EXE202_BE.Data;
using EXE202_BE.Data.DTOS.User;
using EXE202_BE.Data.Models;
using EXE202_BE.Service.Interface;
using EXE202_BE.Service.Services;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Repository.Repositories;
using Google.Apis.Auth.OAuth2;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EXE202_BE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            try
            {
                var firebaseCred = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIALS");
                if (string.IsNullOrEmpty(firebaseCred))
                    throw new ArgumentNullException("FIREBASE_CREDENTIALS", "Environment variable is not set.");
            
                GoogleCredential credential;
            
                if (File.Exists(firebaseCred)) // Local: file path
                {
                    credential = GoogleCredential
                        .FromFile(firebaseCred)
                        .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
            
                    Console.WriteLine("Loaded Firebase credentials from file: " + firebaseCred);
                }
                else // Cloud env: treat as JSON string
                {
                    credential = GoogleCredential
                        .FromJson(firebaseCred)
                        .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
            
                    Console.WriteLine("Loaded Firebase credentials from JSON environment variable.");
                }
            
                var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
                Console.WriteLine("Firebase initialized successfully. Access token acquired.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Firebase initialization failed: {ex.Message}");
                throw;
            }

            // Add DbContext
            // builder.Services.AddDbContext<AppDbContext>(options =>
            //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            builder.Services.AddIdentity<ModifyIdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.
                        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    options.SignIn.RequireConfirmedAccount = false; // Không yêu cầu xác nhận tài khoản
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddHostedService<NotificationsBackgroundService>();
            builder.Services.AddHttpContextAccessor();

            // Configure JWT Authentication
            var jwtSecret = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new ArgumentNullException(nameof(jwtSecret), "JWT Secret cannot be null or empty.");
            }

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                    };
                });

            builder.Services.AddAuthorization();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // Add Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Add controllers
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddServices().AddRepositories();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Add logging
            builder.Services.AddLogging(logging => { logging.AddConsole(); });

            var app = builder.Build();

            app.UseCors(x =>
                x.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            // Configure middleware pipeline
            app.UseCors("AllowAll");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Handle OPTIONS requests
            app.Use(async (context, next) =>
            {
                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    return;
                }

                await next();
            });

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            // app.MapGroup("api/identity").MapIdentityApi<ModifyIdentityUser>();
            app.MapControllers();

            // Seed users
            if (app.Environment.IsDevelopment())
            {
                await SeedUsers.InitializeAsync(app.Services);
            }

            await app.RunAsync();
        }
    }
}