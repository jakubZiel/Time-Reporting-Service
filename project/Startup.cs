using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using project.Models.EntityFramework;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace project
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
            services.AddDistributedMemoryCache();

            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddCors(options => options.AddPolicy("TRS-Policy", builder =>
            {
                builder
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();

            }));

            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TRS-API", Version = "v1" });
            });

            services.AddDbContext<TRSDbContext>(options =>
            {
                string connection = "AzureConnection"; 
                options.UseSqlServer(Configuration.GetConnectionString(connection));
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString(connection));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("TRS-Policy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TRS-API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseFileServer(new FileServerOptions 
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFile/build")),
                RequestPath = "/app",
                EnableDefaultFiles = true
            });
 
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthorization();
            
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
