
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using PresistenceLayer;
using PresistenceLayer.Data;
using PresistenceLayer.Repositories;
using ServiceAbstractionLayer;
using ServicesLayer;
using TalabatDemo.CustomMiddleWares;

namespace TalabatDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region services container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(
                options=>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
                );
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper((x)=>{ },typeof(ServiceAssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var seed = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            seed.DataSeedAsync();

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
