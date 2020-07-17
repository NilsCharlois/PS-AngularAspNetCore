using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DutchTreat.Services;

namespace DutchTreat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMailService, NullMailService>();
	        services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/error");
            }
     
            app.UseStaticFiles();
            app.UseNodeModules();
            
            app.UseRouting();

            app.UseEndpoints(cfg=>{
                cfg.MapRazorPages();
                cfg.MapControllerRoute("Fallback","{controller}/{action}/{id?}", new { controller = "App", action = "Index"}); 
            });
        }
    }
}
