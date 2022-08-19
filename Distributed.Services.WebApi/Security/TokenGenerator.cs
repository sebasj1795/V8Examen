
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Security.Transversal.Auth.Entity.jwt;
using System;
namespace Distributed.Services.WebApi.Security
{
    public static class TokenGenerator
    {

        public static IServiceCollection AddCustomAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuration Authentication
            var jwtAppSettingOptions = configuration.GetSection("JwtIssuerOptions");
            services.Configure<JwtIssuerOptions>(jwtAppSettingOptions);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOptions = configuration.GetSection("JwtIssuerOptions").Get<JwtIssuerOptions>();
                    options.ClaimsIssuer = jwtOptions.Issuer;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = jwtOptions.SymmetricSecurityKey,
                        RequireExpirationTime = true,
                        LifetimeValidator = LifetimeValidator,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.SaveToken = true;
                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        var accessToken = context.Request.Query["access_token"];
                    //        // If the request is for our hub...
                    //        var path = context.HttpContext.Request.Path;
                    //        if (!string.IsNullOrEmpty(accessToken) &&
                    //            (path.StartsWithSegments("/notification")))
                    //        {
                    //            // Read the token out of the query string
                    //            context.Token = accessToken;
                    //        }
                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            #endregion Configuracion Tokens Jwt

            return services;
        }

        public static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null && DateTime.UtcNow < expires)
            {
                return true;
            }
            return false;
        }

    }
}
