﻿using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Receipts.QueryHandler.CrossCutting.Config;

namespace Receipts.QueryHandler.CrossCutting.Extensions.HealthCheckers
{
    public static class HealthCheckersExtensions
    {
        private const string UrlHealthCheck = "/health";
        public static IServiceCollection AddHealthCheckers(this IServiceCollection services, Settings settings)
        {
            services
                .AddHealthChecks()
                .AddMongoDb(settings!.MongoSettings!.ConnectionString, name: "MongoDB")
                .AddUrlGroup(new Uri(settings.SpendManagementIdentity!.Url + UrlHealthCheck), name: "SpendManagement.Identity");

            services
                .AddHealthChecksUI()
                .AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder UseHealthCheckers(this IApplicationBuilder app)
        {
            app
                .UseHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                })
                .UseHealthChecksUI(options => options.UIPath = "/monitor");

            return app;
        }
    }
}
