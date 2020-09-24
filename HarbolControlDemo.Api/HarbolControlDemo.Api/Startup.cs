using HarbolControlDemo.DataModels.Models;
using HarbolControlDemo.Repository.Interface;
using HarbolControlDemo.Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HarbolControlDemo.Api
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
            services.Configure<AppSettingConfiguration>(Configuration.GetSection("ApplicationSettings"));
            services.AddScoped<IHarborControlRepository, HarborControlRepository>();
            services.AddControllers();
            services.AddCors();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // use for cross domain url access.
            app.UseCors(builder => 
            builder.WithOrigins(Configuration["ApplicationSettings:Client_Url"].ToString())
            .AllowAnyHeader()
            .AllowAnyMethod());
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
