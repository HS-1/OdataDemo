using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using OdataDemo.Models;
using Microsoft.OData.Edm;

namespace OdataDemo
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        //builder.WithOrigins("http://example.com",
                        //                   "http://www.contoso.com");
                        builder.AllowAnyOrigin();
                    });
            });
            services.AddOData();
            services.AddControllers(mvcOptions => 
            mvcOptions.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Enable Cors
            app.UseCors(options =>
            //options.WithOrigins("http://localhost:2470/").AllowAnyMethod()
            options.AllowAnyOrigin().AllowAnyMethod()
            );

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            IEdmModel model = GetEdmModel(app.ApplicationServices);

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().Count().OrderBy().Expand().MaxTop(null);
                routeBuilder.MapODataServiceRoute("odata", "odata", model);
                routeBuilder.EnableDependencyInjection();
            });
        }
        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EntitySet<Student>("Students");
            return builder.GetEdmModel();
        }
    }
}
