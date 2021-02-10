using System;
using System.Text;
using MediaStore.Demo.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MediaStore.Demo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o => o.OutputFormatters.Add(
                new XmlDataContractSerializerOutputFormatter()));
            services.AddSession(
                options =>
                {
                    // Set a short timeout for easy testing.
                    options.IdleTimeout = TimeSpan.FromSeconds(10);
                    options.Cookie.HttpOnly = true;
                });

            services.AddDbContext<MediaStoreContext>(
                options =>
                {
                    options.UseSqlite("Data Source=mediaStore.db");
                });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(jwtBearerOptions =>
                    {
                        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateActor = true,
                            ValidateAudience = true,
                            ValidateLifetime = false,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "automatetheplanet.com",
                            ValidAudience = "automatetheplanet.com",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BellatrixIsABeautifulChick")),
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();
            app.UseAuthentication();
            ////app.UseWhen(x => (x.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase)),
            ////builder =>
            ////{
            ////    builder.UseMiddleware<AuthenticationMiddleware>();
            ////});
            app.UseSession();
            app.UseMvc();
        }
    }
}
