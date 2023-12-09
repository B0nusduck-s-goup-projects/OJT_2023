using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

using DataAccessLayer.Repository.Interface;
using DataAccessLayer.Repository;

using BusinessLayer.Service.Interface;
using BusinessLayer.Service;

using BusinessLayer.Mapper;
using BusinessLayer.Mapper.Interface;
using Microsoft.AspNetCore.Routing.Template;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //register database
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(@"connection string"));

            //register repository and their respective interface
            builder.Services.AddScoped<IRepository, Repository>();

            //register service and their respectivev interface
            builder.Services.AddScoped<IService, Service>();

            //register mapper and their respectivev interface
            builder.Services.AddScoped<IMapper, Mapper>();

            //specify routes for api url

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
            /*app.MapControllerRoute(
                name: "default route",
                pattern: "{controller}/{action}/{id?}"
                );*/

            app.Run();
        }
    }
}