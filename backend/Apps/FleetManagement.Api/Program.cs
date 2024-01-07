
using FleetManagement.Api.AutoMapper;
using FleetManagement.Api.Extensions;
using FleetManagement.Api.MediatR.Behaviors;
using FleetManagement.Api.Middleware;
using FluentValidation;
using FM.Domain.Interfaces;
using FM.Infrastructure.EFRepositories;
using FM.Infrastructure.EntityFramework.Context;
using FM.Infrastructure.Services;
using FM.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace FleetManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ----ADD SERVICES-----!

            // Context
            if(ContextExtension.IsDocker)
            {
                builder.WebHost.UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseUrls("http://*:5210;https://*:443");
            }
            

            // Logging
            builder.Services.AddLogging(loggingBuilder =>
            {
                var configuration = new ConfigurationBuilder()
                                                    .AddJsonFile("appsettings.json")
                                                    .Build();
                var serilogPath = configuration["Serilog:WriteTo:0:Args:path"];
                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .WriteTo.File(serilogPath!, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 5)
                    .WriteTo.Seq(configuration["Seq:Nathalie"]!)
                    .CreateLogger();
                loggingBuilder.AddSerilog(logger, dispose: true);
            });

            // JWT-Authentication
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.None;
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<FleetManagementDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:PrivateKey"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents // Get token from cookie
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["accessToken"];
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();

            // DbContext & Repositories 
            builder.Services.AddDbContext<FleetManagementDbContext>()
                .AddScoped<IBestuurderRepository, EFBestuurderRepository>()
                .AddScoped<IBrandstofTypeRepository, EFBrandstofTypeRepository>()
                .AddScoped<IFleetMemberRepository, EFFleetMemberRepository>()
                .AddScoped<ITankkaartRepository, EFTankkaartRepository>()
                .AddScoped<ITypeRijbewijsRepository, EFTypeRijbewijsRepository>()
                .AddScoped<ITypeWagenRepository, EFTypeWagenRepository>()
                .AddScoped<IVoertuigRepository, EFVoertuigRepository>()
                .AddScoped<IUserRepository, EFUserRepository>()
                ;

            // UserService - JwtManager
            builder.Services.AddScoped<IJWTManager, JWTManager>();
            builder.Services.AddScoped<IEncryptedUserService, EncryptedUserService>();

            // MediatR
            builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            //FluentValidation
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            // Keep controller-action names as declared (for CreatedAtAction(...) )
            builder.Services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            // Controllers
            builder.Services.AddControllers();


            // Header Security
            var headers = new Dictionary<string, string>()
            {
                {"X-Frame-Options", "DENY" },
                {"X-Xss-Protection", "1; mode=block"},
                {"X-Content-Type-Options", "nosniff"},
                {"Referrer-Policy", "no-referrer"},
                {"X-Permitted-Cross-Domain-Policies", "none"},
                {"Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()"},
                {"Content-Security-Policy", "default-src 'self'; ; connect-src http://localhost:53834 http://localhost:5210 ws://localhost:53834 wss://localhost:44320;style-src 'unsafe-inline' http://localhost:5210; script-src 'unsafe-inline' http://localhost:5210; img-src https://avatars3.githubusercontent.com 'self' data:"},
            };

            // For full control
            builder.Services.AddAntiforgery(options =>
            {
                options.SuppressXFrameOptionsHeader = true;
            });

            // RateLimitor
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 100,
                            QueueLimit = 0,
                            Window = TimeSpan.FromMinutes(1)
                        }));
            });

            // Cross Origin Resource Sharing
            {
                Console.WriteLine("Cors active");
                // Adding CORS Policy
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowOrigin", builder =>
                    {
                        builder.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithHeaders("Authorization", "Content-Type")
                            .AllowCredentials()
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
                    });
                });
            }

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FleetManagement By CyberSentinels", Version = "v1" });
            });

            // HealthCheck
            builder.Services.AddHealthChecks()
                .AddDbContextCheck<FleetManagementDbContext>();
            builder.Services.AddHealthChecksUI().AddInMemoryStorage();

            // ----BUILD-----

            var app = builder.Build();



            // ----USE-----

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!ContextExtension.IsDocker)
            {
                app.UseHttpsRedirection();
            }
            


            // All pages should be served over https in production "Release" mode:
            if (!builder.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            // Middleware to control headers...
            app.Use(async (context, next) =>
            {
                foreach (var keyvalue in headers)
                {
                    if (!context.Response.Headers.ContainsKey(keyvalue.Key))
                    {
                        context.Response.Headers.Append(keyvalue.Key, keyvalue.Value);
                    }
                }
                await next(context);
            });




            app.UseCors("AllowOrigin");
            
            // Middleware for exceptions
            app.UseMiddleware<ExceptionHandlingMiddleware>();



           // app.UseHealthChecks("/working", options);

            // Sequence important for auth !!
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            // -----------------------------
            app.MapControllers();


            app.UseRateLimiter();

            var options = new HealthCheckOptions
            {
                ResponseWriter = async (c, r) =>
                {
                    c.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new
                    {
                        status = r.Status.ToString(),
                        errors = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
                    });
                    await c.Response.WriteAsync(result);
                }
            };

            app.UseEndpoints(config =>
            {
                config.MapHealthChecks("healthz", options); // url: /healthchecks-ui/healtchecks
                config.MapHealthChecksUI();
                config.MapDefaultControllerRoute();
            });


            app.Run();
        }
    }
}