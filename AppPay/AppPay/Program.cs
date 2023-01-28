using AppPay.DAL;
using AppPay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppPay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Feature}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}