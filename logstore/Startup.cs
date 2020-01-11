using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logstore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using logstore.Auth;

namespace logstore
{
    public class Startup
    {
        private readonly string _secretJwt;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _secretJwt = configuration.GetSection("TokenConfigurations:secret").Value;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("home")));
            services.AddScoped<DataContext, DataContext>();

            //injeta o servico de token
            services.AddScoped<TokenService, TokenService>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(_secretJwt);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // services.AddSwaggerGen(
            //  swagger =>
            //  {
            //      swagger.SwaggerDoc("v1",
            //          new Info
            //          {
            //              Title = "API de POC da logstore",
            //              Version = "v1",
            //              Description = "Projeto desenvolvido em Asp.Net CORE"
            //          });
            //  }
            //  );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseSwagger();
            // app.UseSwaggerUI(
            //     swagger =>
            //     {
            //         swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto");
            //     }
            //     );

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
