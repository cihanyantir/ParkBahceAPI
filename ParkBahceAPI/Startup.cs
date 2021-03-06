using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ParkBahceAPI.Abstract;
using ParkBahceAPI.Data;
using ParkBahceAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParkBahceAPI.MilletMapper;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace ParkBahceAPI
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
            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMilletBahcesiRepository, MilletBahcesiRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddScoped<ISosyalTesisRepository, SosyalTesisRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(MilletMappings));
            services.AddApiVersioning(c =>
            {
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.DefaultApiVersion = new ApiVersion(1, 0);
                c.ReportApiVersions = true;

            });
            services.AddVersionedApiExplorer(c => c.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();


           



            ///////////////////TOKEN/////////////////
            var appsettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appsettingsSection);//for TOKEN 
            var appSettings = appsettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x => //AUTH BEARER
            { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(x=> { x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false


                    };
                })
                ;

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("ParkBahceAPI", new OpenApiInfo { Title = "ParkBahceAPI", Version = "v1" });
            //    //c.SwaggerDoc("ParkBahceAPITrail", new OpenApiInfo { Title = "ParkBahceAPI-Trail", Version = "v1" });
            //    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            //    c.IncludeXmlComments(cmlCommentsFullPath); //addxml
            //}
            
               
            //);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                 app.UseSwaggerUI(c => {  foreach (var desc in provider.ApiVersionDescriptions)
                         c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                         desc.GroupName.ToUpperInvariant());

                  
                });
                //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/ParkBahceAPI/swagger.json", "ParkBahceAPI");
                //    //c.SwaggerEndpoint("/swagger/ParkBahceAPITrail/swagger.json", "ParkBahceAPI Trail");
                //});

            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(
                x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
