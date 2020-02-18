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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Npgsql;

namespace leashApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // touched
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try{ //trying to create and connecft with environemnt variables. This will work build only.
               
                String connectionString = Helpers.connectionStringMaker();
                services.AddDbContext<ParkContext>(opt =>
                opt.UseNpgsql(connectionString));
                services.AddControllers();

            } catch(InvalidOperationException e){

                Console.WriteLine(" Caught exception. opening connection to local db " + e);
                var connectionString = Configuration["PostgreSql:ConnectionString"];
                var dbPassword = Configuration["PostgreSql:DbPassword"];
                var builder = new NpgsqlConnectionStringBuilder(connectionString){
                    Password = dbPassword
                };
                services.AddDbContext<ParkContext>(opt =>
                opt.UseNpgsql(builder.ConnectionString));
                services.AddControllers();
            
            } 

            //Adding authentication
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "api1";
                });
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //added
            try{
                var context = app.ApplicationServices.GetService<ParkContext>();

                if(!context.Database.EnsureCreated()){
                    context.Database.Migrate();
                }

            } catch (InvalidOperationException e) {
                Console.WriteLine("catched contect error: " + e);
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
