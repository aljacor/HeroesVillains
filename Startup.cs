using HeroesVillains.Interfaces;
using HeroesVillains.Models;
using HeroesVillains.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IApi<HeroesResponse>, HeroVillainService<HeroesResponse>>();
            services.AddScoped<IApi<Character>, HeroVillainService<Character>>();
            //services.AddTransient //genera nuevas instancias cada que se hace una llamada
            //services.AddSingleton //Se genera s�lo una unstancia
            services.AddControllers();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HeroVillain}/{action=Index}/{search?}"
                );
                endpoints.MapControllerRoute(
                    name: "CharacterDetail",
                    pattern: "{controller=HeroVillain}/{action=CharacterDetail}/{search?}"
                );

                endpoints.MapControllerRoute(
                    name: "ShowResults",
                    pattern: "HeroVillain/ShowSearchResults"
                );
            });
        }
    }
}
