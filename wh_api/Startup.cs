using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace wh_api
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
            services.AddCors(c => c.AddPolicy("AllowOrigin", Opt => Opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())); ;
            services.AddControllers();
            services.AddControllersWithViews().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling
            = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(opt => opt.SerializerSettings.ContractResolver = new DefaultContractResolver());
            //services.AddMvc(option => option.EnableEndpointRouting = false);
            //services.AddControllers().AddNewtonsoftJson();
            //services.AddCors(); // Make sure you call this previous to AddMvc
            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //          if (env.IsDevelopment())
            //          {
            //              app.UseDeveloperExceptionPage();
            //          }
            //          else
            //          {
            //              app.UseHsts();
            //          }
            //          app.UseCors(
            //    options => options.WithOrigins("your link here").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            //);

            //          app.UseHttpsRedirection();
            //          app.UseMvc();
        }
    }
}
