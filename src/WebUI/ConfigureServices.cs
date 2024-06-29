using BackEnd.Application.Common.Interfaces;
using BackEnd.Infrastructure.Persistence;
using BackEnd.WebUI.Filters;
using BackEnd.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        #region Cors
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
        #endregion

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "BackEnd API";
            // configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            // {
            //     Type = OpenApiSecuritySchemeType.ApiKey,
            //     Name = "Authorization",
            //     In = OpenApiSecurityApiKeyLocation.Header,
            //     Description = "Type into the textbox: Bearer {your JWT token}."
            // });

            // configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }
}
