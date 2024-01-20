using GreatFriends.SmartHoltel.Services.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatFriends.SmartHoltel.APIS
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GreatFriends.SmartHoltel.APIS", Version = "v1" });

                //c.TagActionsBy(api =>
                //{
                //    if (api.GroupName != null)
                //    {
                //        return new[] { api.GroupName };
                //    }

                //    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                //    if (controllerActionDescriptor != null)
                //    {
                //        return new[] { controllerActionDescriptor.ControllerName };
                //    }

                //    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                //});
                //c.DocInclusionPredicate((name, api) => true);
            });

            services.AddDbContext<AppDb>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(AppDb)))
                .UseLazyLoadingProxies();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDb db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreatFriends.SmartHoltel.APIS v1"));
            }
            
           // db.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
