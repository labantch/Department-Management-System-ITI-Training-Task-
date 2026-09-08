using BLogicLayer.Interfaces;
using BLogicLayer.Services;
using DataAccessLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using BLogicLayer.Services;
using Microsoft.AspNetCore.Builder;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register Session
            builder.Services.AddSession();
            // Configure DbContext from configuration (use DefaultConnection in appsettings or environment)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? "Server=.;Database=ITI;Trusted_Connection=True; TrustServerCertificate=true;";
            builder.Services.AddDbContext<ITIDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            // Register trainee and enrollment services
            builder.Services.AddScoped<ITraineeSevice, TraineeService>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Enable Session
            app.UseSession();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}