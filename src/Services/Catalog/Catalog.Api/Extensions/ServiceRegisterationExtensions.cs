using Catalog.Api.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Extensions
{
    public static class ServiceRegisterationExtensions
    {
        public static void SetupSettings(this IServiceCollection service, IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings();
            configuration.GetSection("DatabaseSettings").Bind(databaseSettings);
            service.AddSingleton(databaseSettings);
        }
    }
}
