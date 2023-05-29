using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleBigTraveler.Models.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LittleBigTraveler
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // Appel de méthodes que l'on veut au demarrage (initialize, create, delete...)

            //using (Dal dal = new Dal())
            //{
            //    dal.DeleteCreateDatabase();
            //}


            using (BddContext ctx = new BddContext())
            {
                ctx.InitializeDb();
            }

            //Div
            app.UseRouting();
            app.UseStaticFiles();

            //Ce que l'on affiche au demarrage 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

