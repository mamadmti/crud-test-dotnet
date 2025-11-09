using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Mc2.CrudTest.Infrastructure.Data;
using Mc2.CrudTest.Infrastructure.Repositories;
using Mc2.CrudTest.Domain.Interfaces;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Mc2.CrudTest.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? "Server=localhost,1433;Database=CustomerDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true"));

            // Register repositories
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Register MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
                typeof(Mc2.CrudTest.Application.Commands.CreateCustomerCommand).Assembly));

            // Register FluentValidation
            builder.Services.AddValidatorsFromAssembly(
                typeof(Mc2.CrudTest.Application.Commands.CreateCustomerCommand).Assembly);

            // Register Validation Pipeline Behavior
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(Mc2.CrudTest.Application.Behaviors.ValidationBehavior<,>));

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}