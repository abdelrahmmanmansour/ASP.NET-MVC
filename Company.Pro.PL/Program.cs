using Company.Pro.BLL.Interfaces;
using Company.Pro.BLL.Repositories;
using Company.Pro.DAL.Data.Contexts;
using Company.Pro.PL.Mapping;
using Company.Pro.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Pro.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Built-in MVC Service
            builder.Services.AddScoped<IDepartmentRepository,DepartmentReository>(); // Dependency Injection For DepartmentReository Class + Interface not concrete class
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Dependency Injection For EmployeeRepository Class + Interface not concrete class
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>(); // Dependency Injection For UnitOfWork Class + Interface not concrete class
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile())); // Dependency Injection For AutoMapper Class + MappingProfile Class
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Dependency Injection For CompanyDbContext Class

            // To Allow Dependency Injection
            #region The Differences Between AddScoped,AddTransient,AddSingleton
            // Life Time Of Object:

            // 1. AddScoped => CLR Create Object Per Request(Repository)
            // 2. AddTransient => CLR Create Object Per Operation(Configuration)
            // 3. AddSingleton => CLR Create Object Per Application(Chaching)
            builder.Services.AddScoped<IScooped, Scooped>(); // Dependency Injection For Scooped Class + Interface not concrete class
            builder.Services.AddTransient<ITransiant, Transiant>(); // Dependency Injection For Transiant Class + Interface not concrete class
            builder.Services.AddSingleton<ISingelton, Singelton>(); // Dependency Injection For Singelton Class + Interface not concrete class
            #endregion

            // The Best Practice To performance The Application:
            // Make Any Function (Syncronous) => (ASyncronous) => (await)
            // In => BLL + PL

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Department}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
