using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Security.Infrastructure.DataModel.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Security.Transversal.IoC;
using Distributed.Services.WebApi.Security;
using Security.Transversal.Mapper;
using Security.Transversal.Common.Wapper;
using Security.Transversal.Logger;
using Serilog;
using Hangfire;
using Security.Job.Configuration;
using Security.Transversal.Common.JsonConverter;
using System.Linq;

namespace Distributed.Services.WebApi
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
            services.AddDbContext<MainContext>(opts =>
                opts.UseSqlServer(Configuration["ConnectionStrings:CnnSqlServer"]));

            services.Configure<LoginLockSetting>(Configuration.GetSection("LoginLockSettings"));
            services.Configure<EmailSetting>(Configuration.GetSection("EmailSettings"));

            //Configuración lectura archivo
            Environment.CurrentDirectory = AppContext.BaseDirectory;
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            services.AddSingleton<IConfiguration>(configuration);

            //ALMACENAR CLAIMS TOKEN
            services.AddHttpContextAccessor();

            //INJECTIONS 
            services.AddInjectionsServices();
            services.AddInjectionsJobs();

            //AUTHENTICATION
            services.AddCustomAuthenticationJwt(Configuration);

            //CORS
            #region Generate Cors
            services.AddCors(opt =>
            {
            var urlList = Configuration.GetSection("AllowedOrigin").GetChildren().ToArray()
                .Select(c => c.Value?.TrimEnd('/')).ToArray();

                opt.AddPolicy("CorsPolicy",
                                        builder =>
                                        {
                                            //builder.WithOrigins("*", "http://localhost", "https://localhost", "http://localhost:4200/dashboard");
                                            //builder.AllowAnyOrigin()
                                            //    .AllowAnyMethod()
                                            //    .AllowAnyHeader();
                                            builder.WithOrigins(urlList)
                                                   .AllowAnyMethod()
                                                   .AllowAnyHeader()
                                                   .AllowCredentials();
                                        }
                                    );
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(
            //        name: "AllowOrigin",
            //        builder => {
            //            builder.AllowAnyOrigin()
            //                    .AllowAnyMethod()
            //                    .AllowAnyHeader();
            //        });
            //});

            #endregion
            //ROUTING
            services.AddRouting();
            //AUTOMAPPER
            services.CustomAutomapper();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DatetimeConverter());
                options.JsonSerializerOptions.Converters.Add(new TrimStringConverter());
            });

            //HANGFIRE
            services.AddHangfireConfiguration(Configuration);

            //SWAGGER
            #region Configuration Swagger
            services.AddSwaggerGen(c =>
            {
                #region INFORMATION
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Examen demo",
                    Version = "v1",
                    Description = "Muestra de Swagger",
                    //TermsOfService = new Uri("http://localhost:1234/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Joel Sebastian",
                        Email = "joel.sebastian.a@gmail.com",
                        Url = new Uri("http://localhost:1234/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                #endregion

                #region ADD_OPTION_AUTHORIZATION_JWT_BEARER
                var securitySchemaBearer = new OpenApiSecurityScheme
                {
                    Description = @"JWT Encabezado de autorización utilizando el esquema Bearer.
                        <br />Ingrese 'Bearer' [espacio] y luego su token en la entrada de texto a continuación.
                        <br />Ejemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchemaBearer);
                #endregion
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchemaBearer, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                //c.EnableAnnotations();
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, 
            IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Distributed.Services.WebApi v1"));
            //}

            //app.UseHttpsRedirection();

            #region Logger Configuration
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration.GetSection("Logging"))
            .Enrich.FromLogContext()
            .CreateLogger();
            loggerFactory.AddSerilog();

            app.UseMiddlewareLogging();
            #endregion

            #region Hangfire
            app.UseHangfireDashboard();
            #endregion

            //Use Cors
            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireMethods();
            app.UseHttpsRedirection();
        }
    }
}
