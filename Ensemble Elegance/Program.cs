using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ensemble_Elegance.Models;

using System.Web.Mvc;

namespace Ensemble_Elegance
{

    public class Program
    {

        // Roles list
        static List<IdentityRole> identityRoles = new()
        {
            new IdentityRole("Admin")
        };

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<UserModel, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Встановлюємо таймаут сесії
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();



            // Roles initialization and creating if doesn't exists
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in identityRoles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(name: "defaultRoute",
                pattern: "/",
                defaults: new { controller = "Home", action = "Index" });


            app.MapControllerRoute(name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}