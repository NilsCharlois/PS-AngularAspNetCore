using DutchTreat.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DutchTreat.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;
using System.Reflection;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchTreat.Data.DutchContext>(cfg=>{
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });
            
            services.AddTransient<IMailService, NullMailService>();

	        services.AddTransient<DutchTreat.Data.DutchSeeder>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IDutchRepository, DutchRepository>();
            
            services.AddControllersWithViews();

            services.AddRazorPages().AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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
