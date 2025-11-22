
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using PresistenceLayer;
using PresistenceLayer.Data;
using PresistenceLayer.Repositories;
using ServiceAbstractionLayer;
using ServicesLayer;
using Shared.ErrorModels;
using TalabatDemo.CustomMiddleWares;
using TalabatDemo.Factories;
using TalabatDemo.Extentions;

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

            builder.Services.AddSwaggerServices(builder.Configuration);  // extension method for swagger services

            builder.Services.AddValidationServices(); // extension method for validation services

            builder.Services.AddInfrastructureServices(builder.Configuration); // extension method for infrastructure services // Configuration for GetConnectionString

            builder.Services.AddApplicationServices(); // extension method for application services

            #endregion

            var app = builder.Build();

            app.SeedDataAsync(); // extension method for seeding data

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
