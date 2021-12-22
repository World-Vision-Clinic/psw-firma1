using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hospital.SharedModel;
using Hospital.MedicalRecords.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hospital_API
{
    public class Startup
    {
        private const string SECRET = "ecc0024b9feaa167cf8c5bc4819bc03aa8ed88d86524bd647db6f3363dfabd13";
        public static readonly SymmetricSecurityKey KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", jwtOptions => 
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = KEY,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "PSW",
                    ValidateIssuer = true,
                    ValidAudience = "PSW-audience",
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });
           //services.AddDbContext<HospitalContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString(CreateConnectionStringFromEnvironment())));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static string CreateConnectionStringFromEnvironment()
        {
            var server = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
            //var database = EnvironmentConnection.GetSecret("DATABASE_SCHEMA") ?? "MyWebApi.Dev";
            var database = Environment.GetEnvironmentVariable("DATABASE_SCHEMA") ?? "MyWebApi.Dev";
            //var user = EnvironmentConnection.GetSecret("DATABASE_USERNAME") ?? "admin";
            var user = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "admin";
            //var password = EnvironmentConnection.GetSecret("DATABASE_PASSWORD") ?? "ftn";
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "ftn";
            var integratedSecurity = Environment.GetEnvironmentVariable("DATABASE_INTEGRATED_SECURITY") ?? "true";
            var pooling = Environment.GetEnvironmentVariable("DATABASE_POOLING") ?? "true";

            string retVal = "Server=" + server + ";Port=" + port + ";Database=" + database + ";User ID=" + user + ";Password=" + password + ";Integrated Security=" + integratedSecurity + ";Pooling=" + pooling + ";";
            return retVal;
        }
    }
}
