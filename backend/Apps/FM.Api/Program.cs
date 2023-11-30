using EF_Infrastructure.Context;
using EF_Repositories;
using FM_API;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

namespace FM.Api.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Work in Progress
            //builder.Services.AddLogging(loggingBuilder =>
            //{
            //    var configuration = new ConfigurationBuilder()
            //                                        .AddJsonFile("appsettings.json")
            //                                        .Build();
            //    var logger = new LoggerConfiguration()
            //        .ReadFrom.Configuration(configuration)
            //        .CreateLogger();
            //    loggingBuilder.AddSerilog(logger, dispose: true);
            //});


            // Add services to the container.
            builder.Services.AddDbContext<FleetManagementDbContext>()
                  .AddScoped<IFMBestuurderRepository, EFBestuurderRepository>()
                  .AddScoped<IFMTankkaartRepository, EFTankkaartRepository>()
                  .AddScoped<IFMBrandstoftypeRepository, EFBrandstofTypeRepository>()
                  .AddScoped<IFMLoginRepository, EFLoginRepository>()
                  .AddScoped<IFMFleetRepository, EFFleetRepository>()
                  .AddScoped<IFMVoertuigRepository, EFVoertuigRepository>()
                  .AddScoped<IFMTypeWagenRepository, EFTypeWagenRepository>()
                  .AddScoped<IFMTypeRijbewijsRepository, EFTypeRijbewijsRepository>();

            builder.Services.AddScoped<JWTokenService>();

            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddControllers();

            // HealthCheck for EntityFramework
            builder.Services.AddHealthChecks()
                    .AddDbContextCheck<FleetManagementDbContext>();

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

            // Add JWT Authentication - Work in Progress
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                var configuration = new ConfigurationBuilder()
                                                            .AddJsonFile("appsettings.json")
                                                            .Build();
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = "http://localhost:5100/",
                                    ValidAudience = "http://localhost:5100/",
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RanmKUsWWkCdQohoQy2UitQx2MKr5R0m"))
                                };
                            });

            builder.Services.AddAuthorization();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //CORS
            {
                Console.WriteLine("Cors active");
                // Adding CORS Policy
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowOrigin", builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        // .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
                    });
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapHealthChecks("/healtz");

            app.UseRateLimiter();

            app.UseCors("AllowOrigin");

            app.Run();
        }
    }
}