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
using Swashbuckle.AspNetCore.SwaggerGen;
using CloudinaryDotNet;
using EXE202_BE.Data.SeedData;
using EXE202_BE.Utilities;
using Serilog;
using Serilog.AspNetCore;
using System;
using Hangfire;
using Hangfire.PostgreSql;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

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
                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential =                     credential = GoogleCredential
                            .FromFile(firebaseCred)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Firebase initialization failed: {ex.Message}");
                throw;
            }

            // Cloudinary (từ file mới)
            var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
            if (string.IsNullOrEmpty(cloudinaryUrl))
            {
                throw new Exception("CLOUDINARY_URL environment variable is not set.");
            }else if (cloudinaryUrl == string.Empty)
            {
                Console.WriteLine("⚠️ CLOUDINARY_URL is not set. Using dummy Cloudinary instance.");

                var dummyAccount = new Account("dummy", "dummy", "dummy");
                var dummyCloudinary = new Cloudinary(dummyAccount);
                builder.Services.AddSingleton(dummyCloudinary);
            }


            var uri = new Uri(cloudinaryUrl);
            var userInfo = uri.UserInfo.Split(':');

            var account = new Account(
                uri.Host,        // CloudName
                userInfo[0],     // API Key
                userInfo[1]      // API Secret
            );

            builder.Services.AddSingleton(new Cloudinary(account));

            // Add DbContext
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
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Thêm Hangfire
            builder.Services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
                }));

            // Thêm Hangfire Server
            builder.Services.AddHangfireServer();

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

            // Add Swagger with file upload support
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EXE202_BE API", Version = "v1" });

                // Add JWT security definition
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
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

                // Configure Swagger to handle file uploads
                c.MapType<IFormFile>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });

                // Apply the custom operation filter for file uploads
                c.OperationFilter<FileUploadOperationFilter>();
            });

            // Add controllers
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddServices().AddRepositories();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IUserProfilesService, UserProfilesService>();
            builder.Services.AddScoped<SubscriptionExpirationJob>();

            // Add Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger();

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
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EXE202_BE API v1");
                });
            }

            // Thêm Hangfire Dashboard
            app.UseHangfireDashboard("/hangfire");

            // Đăng ký các job định kỳ
            using (var scope = app.Services.CreateScope())
            {
                var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
                var subscriptionExpirationJob = scope.ServiceProvider.GetRequiredService<SubscriptionExpirationJob>();

                recurringJobManager.AddOrUpdate(
                    "CheckSubscriptions",
                    () => subscriptionExpirationJob.CheckAndUpdateExpiredSubscriptions(),
                    Cron.Daily(0, 0)); // Chạy lúc 00:00 hàng ngày

                recurringJobManager.AddOrUpdate(
                    "NotifyExpiringSubscriptions",
                    () => subscriptionExpirationJob.NotifyExpiringSubscriptions(),
                    Cron.Daily(8, 0)); // Chạy lúc 08:00 hàng ngày
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Seed users and ingredients
            if (app.Environment.IsDevelopment())
            {
                await SeedUsers.InitializeAsync(app.Services);
                await SeedIngredients.InitializeAsync(app.Services);
            }

            await app.RunAsync();
        }
    }
}