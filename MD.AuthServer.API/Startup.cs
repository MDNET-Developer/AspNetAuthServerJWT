using MD.AuthServer.Core.Configration;
using MD.AuthServer.Core.Model;
using MD.AuthServer.Core.Repositries;
using MD.AuthServer.Core.Service;
using MD.AuthServer.Data;
using MD.AuthServer.Data.Repositories;
using MD.AuthServer.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SharedLiblary.Confuguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MD.AuthServer.API
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
        {//DI Regiater
            services.AddScoped<IAuthenticationService, AutheticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalDb"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("MD.AuthServer.Data");
                });
            });


            services.AddIdentity<UserApp, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;  
                //sifre sifirlama ucun adddefaulttokenproviders yazildi
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<CustomTokenOptions>(Configuration.GetSection("TokenOption"));

            services.Configure<Client>(Configuration.GetSection("Clients"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOptions>();
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audence[0],
                    IssuerSigningKey=SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience=true,
                    ValidateIssuer=true,
                    ValidateLifetime=true,
                    //Umumi vaxt(Bolgeler arasi zaman ferqi levg edir)
                    ClockSkew=TimeSpan.Zero,
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MD.AuthServer.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MD.AuthServer.API v1"));
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
