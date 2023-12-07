using System.Text.Json;
using AspNetCoreTrainingProject.Contracts.HealthChecks;
using AspNetCoreTrainingProject.Data;
using AspNetCoreTrainingProject.Extensions;
using AspNetCoreTrainingProject.Installers;
using AspNetCoreTrainingProject.Options;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTrainingProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.InstallServicesInAssembly(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var swaggerOptions = new SwaggerOptions();
            builder.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseHealthChecks();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
                option.RoutePrefix = string.Empty;
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            if (builder.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<DataContext>();

                    // Apply the migrations
                    //context.Database.Migrate();

                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        var adminRole = new IdentityRole("Admin");
                        await roleManager.CreateAsync(adminRole);
                    }


                    if (!await roleManager.RoleExistsAsync("NormalUsers"))
                    {
                        var normalUserRole = new IdentityRole("NormalUsers");
                        await roleManager.CreateAsync(normalUserRole);
                    }
                }
            }

            app.Run();
        }
    }
}