
using EF_Repositories;
using EF_Infrastructure.Context;
using FM_API;


namespace FM.Api.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<FleetManagementDbContext>()
                  .AddScoped<EFBestuurderRepository>()
                  .AddScoped<EFTankkaartRepository>()
                  .AddScoped<EFBrandstofTypeRepository>()
                  .AddScoped<EFLoginRepository>();


            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


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

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("AllowOrigin");

            app.Run();
        }
    }
}