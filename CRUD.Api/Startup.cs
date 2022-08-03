using AutoMapper;
using CRUD.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace CRUD.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public class ApplicationLogs
        {
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();
            services.AddSingleton(typeof(ILogger), logger);


            services.AddDbContext<CrudDbContext>(cfg => 
            cfg.UseSqlServer(Configuration.GetConnectionString("CrudConnection")));

            services.AddScoped<ICrudRepository, CrudRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors(opt =>
                opt.AddPolicy("AllowWebApp",
                builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(Configuration["ClientDomain"])));
            services.AddMvc();
            services.AddControllers().AddNewtonsoftJson();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() == true)
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowWebApp");
            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
