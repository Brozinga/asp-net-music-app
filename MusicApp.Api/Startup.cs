using System.Text;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MusicApp.Api.Mapper;
using MusicApp.Infrastructure.Contexts;
using MusicApp.Shared.DI;
using Newtonsoft.Json;


namespace MusicApp.Api
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
            services.AddDbContext<SqliteContext>
                (x => x.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));

            IdentityInjection.ConfigureEngine(services);
            IdentityInjection.ConfigurePassword(services);
            IdentityInjection.ConfigurePolicies(services);

            ClassInjection.ConfigureRepository(services);
            ClassInjection.ConfigureHandle(services);

            AutoMapperConfig.Configuration(services);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicApp.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
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


            services.AddCors(o => o.AddPolicy("Geral", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyOrigin();
                builder.Build();
            }));

            services.AddControllers(opt =>
                {

                    opt.RespectBrowserAcceptHeader = true;
                    opt.ReturnHttpNotAcceptable = true;
                    opt.OutputFormatters.Clear();
                    opt.InputFormatters.Clear();
                })
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Uma das formas de Resolver o Cors e os Headers;
            //app.UseCors(o => o.SetIsOriginAllowed(x => _ = true).AllowAnyHeader());

            app.UseAllElasticApm();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicApp.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("Geral");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
