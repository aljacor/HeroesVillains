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
            services.AddCors(o => o.AddPolicy("HERO_VILLAIN_CORS", builder =>
            {
                builder.WithOrigins("http://localhost:44382/")
                .SetIsOriginAllowed(origin => true).AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped<IApi<HeroesResponse>, HeroVillainService<HeroesResponse>>();
            services.AddScoped<IApi<Character>, HeroVillainService<Character>>();
            //services.AddTransient //genera nuevas instancias cada que se hace una llamada
            //services.AddSingleton //Se genera sólo una unstancia
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
                //Catch error not found
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("HERO_VILLAIN_CORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HeroVillain}/{action=Index}/{search?}"
                );
                endpoints.MapControllerRoute(
                    name: "CharacterDetail",
                    pattern: "Character/{search?}",
                    defaults: new { controller = "HeroVillain", action = "CharacterDetail" }
                );

                endpoints.MapControllerRoute(
                    name: "ShowResults",
                    pattern: "HeroVillain/ShowSearchResults"
                );

                endpoints.MapControllerRoute(
                    name: "Error",
                    pattern: "{controller=Error}/{action=Error}/{search?}"
                );
            });
        }
    }
}
