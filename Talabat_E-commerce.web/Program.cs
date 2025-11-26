
using AutoMapper;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using Service;
using Service.MappingProfile;
using ServiceAbstraction;
using Shared.ErrorModels;
using System.Reflection;
using Talabat_E_commerce.web.CustomMiddleWare;
using Talabat_E_commerce.web.Extensions;
using Talabat_E_commerce.web.Factories;

namespace Talabat_E_commerce.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container. 

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAplicationServices();

            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            await app.SeedDataBaseAsync();

            #region Configure the HTTP request pipeline.
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            #endregion
            app.Run();
        }
    }
}
