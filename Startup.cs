using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleBigTraveler.Models.DataBase;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LittleBigTraveler
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/User/LogIn";
                });

            services.AddScoped<TravelDAL>();
            services.AddScoped<DestinationDAL>();
            services.AddScoped<PackageDAL>();
            services.AddScoped<BookingDAL>();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (BddContext ctx = new BddContext())
            {
                ctx.InitializeDb();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=IndexTEST}/{id?}");

                endpoints.MapControllerRoute(
                    name: "confirmation",
                    pattern: "Booking/Confirmation/{bookingId}",
                    defaults: new { controller = "Booking", action = "Confirmation" }
                );

                endpoints.MapControllerRoute(
                    name: "delete",
                    pattern: "Booking/Delete/{bookingId}",
                    defaults: new { controller = "Booking", action = "Delete" }
                );

                //endpoints.MapControllerRoute(
                //    name: "logoutOnStartup",
                //    pattern: "LogoutOnStartup",
                //    defaults: new { controller = "User", action = "LogoutOnStartup" });

        });
        }

    }
}
