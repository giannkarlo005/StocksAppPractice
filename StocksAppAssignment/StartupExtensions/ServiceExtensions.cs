﻿using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using StocksAppAssignment.Core.RepositoryContracts;
using StocksAppAssignment.Core.Services;
using StocksAppAssignment.Core.ServiceContracts;
using StocksAppAssignment.Infrastructure.DatabaseContext;
using StocksAppAssignment.Infrastructure.Repository;
using StocksAppAssignment.Core.Identities;

namespace StocksAppAssignment.UI.StartupExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServicesExtension(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));

                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddXmlSerializerFormatters();

            services.AddTransient<IJwtService, JwtService>();

            services.AddApiVersioning(config =>
            {
                //Reads version number from request URL at "apiVersion" constraint
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.Configure<FinnhubApiOptions>(configuration.GetSection("finnhubapi"));

            services.AddScoped<IFinnhubService, FinnhubService>();
            services.AddScoped<ICreateStockOrdersService, CreateStockOrdersService>();
            services.AddScoped<IGetStockOrdersService, GetStockOrdersService>();
            services.AddScoped<IStockMarketRepository, StockMarketRepository>();

            services.AddDbContext<StockMarketDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {

            })
              .AddEntityFrameworkStores<StockMarketDbContext>()
              .AddDefaultTokenProviders()
              .AddUserStore<UserStore<ApplicationUser, ApplicationRole, StockMarketDbContext, Guid>>()
              .AddRoleStore<RoleStore<ApplicationRole, StockMarketDbContext, Guid>>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],

                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            services.AddAuthorization(options => {
            });

            //Swagger
            //Generates description for all endpoints
            services.AddEndpointsApiExplorer();
            //Generates OpnAPI Specification
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Stocks App",
                    Version = "1.0"
                });

                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Stocks App",
                    Version = "2.0"
                });
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            //CORS to localhost
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>())
                           .WithHeaders("Authorization", "origin", "accept", "content-type")
                           .WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            return services;
        }
    }
}
