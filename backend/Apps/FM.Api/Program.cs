
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
            builder.Services.AddDbContext<FleetManagementContext>()
                  .AddScoped<EFBestuurderRepository>();


            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
             
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(builder =>
            //TODO eens naar productie moet cors policy aangepast worden om enkel CRUD toe te laten vanop SPA
                {
                    builder.WithOrigins("http://localhost:5173/")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });

            app.MapControllers();


            app.Run();
        }
    }
}