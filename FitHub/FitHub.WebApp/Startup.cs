using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.WebApp.Data;
using FitHub.WebApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace FitHub.WebApp
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
            services.Configure<AuthOptions>(Configuration.GetSection(nameof(AuthOptions)));

            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<DataBaseContext>(options => options
                        .UseNpgsql(Configuration.GetConnectionString("PostgreSQL")));

            //services.AddEntityFrameworkNpgsql()
            //        .AddDbContext<DataBaseContext>(options => options
            //            .UseSqlServer(Configuration.GetConnectionString("LocalDB")));

            services.AddIdentity<Customer, IdentityRole<Guid>>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequiredLength = 0;
                })
                    .AddEntityFrameworkStores<DataBaseContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration.GetSection("AuthOptions")["ISSUER"],

                            ValidateAudience = true,
                            ValidAudience = Configuration.GetSection("AuthOptions")["AUDIENCE"],

                            ValidateLifetime = true,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(Configuration.GetSection("AuthOptions")["KEY"])
                        };
                    });

            services.AddMvc(opt => opt.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
