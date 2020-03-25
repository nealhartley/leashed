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
            // var connectionString = Configuration["PostgreSql:ConnectionString"];
            // var dbPassword = Configuration["PostgreSql:DbPassword"];
            // var builder = new NpgsqlConnectionStringBuilder(connectionString){
            //     Password = dbPassword
            // };

             Console.WriteLine("About to make the string for db");
            //  Console.WriteLine(Helpers.connectionStringMaker());

            try{ //trying to create and connecft with environemnt variables. This will work build only.
                Console.WriteLine(" Attempting connection to db with environment vars ");

                String connectionString = Helpers.connectionStringMaker();
                Console.WriteLine(" -- we are now past thrown error ");
                services.AddDbContext<ParkContext>(opt =>
                opt.UseNpgsql(connectionString));
                services.AddControllers();

            } catch(InvalidOperationException e){

                Console.WriteLine(" -- Caught exception. opening connection to local db " + e);
                Console.WriteLine(" -- --");
                Console.WriteLine(" -- now beginning to build string from config files");
                var connectionString = Configuration["PostgreSql:ConnectionString"];
                var dbPassword = Configuration["PostgreSql:DbPassword"];
                Console.WriteLine("connection string: " + connectionString);
                Console.WriteLine("db password: " + dbPassword);
                var builder = new NpgsqlConnectionStringBuilder(connectionString){
                    Password = dbPassword
                };
                services.AddDbContext<ParkContext>(opt =>
                opt.UseNpgsql(builder.ConnectionString));
                services.AddControllers();
            
            } 

            //services.AddControllers();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("--getting context");
            //added
            try{
                var context = app.ApplicationServices.GetService<ParkContext>();

                if(!context.Database.EnsureCreated()){
                    Console.WriteLine("--about to run migration");
                    context.Database.Migrate();
                }

            } catch (InvalidOperationException e) {
                Console.WriteLine("catched context error: " + e);
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
