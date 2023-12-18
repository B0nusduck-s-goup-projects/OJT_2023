using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

using DataAccessLayer.Repository.Interface;
using DataAccessLayer.Repository;

using BusinessLayer.Service.Interface;
using BusinessLayer.Service;

using BusinessLayer.Mapper;
using Microsoft.AspNetCore.Routing.Template;
using AutoMapper;

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
            option.UseSqlServer(
                @"Server=ADMIN\SQLEXPRESS;User ID=MasterAdmin1;Password=MasterAdmin1;Database=StudentManage;TrustServerCertificate=True;"
            ));

            //register repository and their respective interface
            builder.Services.AddScoped<ISubjectStudentRepository, SubjectStudentRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<IProfessorContactRepository, ProfessorContactRepository>();
            builder.Services.AddScoped<IStudentContactRepository, StudentContactRepository>();

            //register service and their respective interface
            builder.Services.AddScoped<ISubjectStudentService, SubjectStudentService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IProfessorService, ProfessorService>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<IProfessorContactService, ProfessorContactService>();
            builder.Services.AddScoped<IStudentContactService, StudentContactService>();

            //auto mapper configuration

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //add content negotiation
            builder.Services.AddControllers().AddXmlSerializerFormatters();
            //only return available data format
            builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //specify routes for api url
            app.MapControllers();
            /*app.MapControllerRoute(
                name: "default route",
                pattern: "{controller}/{action}/{id?}"
                );*/

            app.Run();
        }
    }
}
