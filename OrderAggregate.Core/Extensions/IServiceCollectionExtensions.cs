using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderAggregate.Core.Interfaces;
using OrderAggregate.Core.Models;
using OrderAggregate.Core.Repositories;
using OrderAggregate.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// register all necessary services for OrderAggregateCode functionality
        /// </summary>
        public static IServiceCollection UseOrderAggregate(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRepository<Order>, OrderInMemoryRepository>();
            services.AddTransient<OrderAggregatorService>();
            services.AddTransient<ConfigurationService>();
            services.AddHostedService<SenderService>();

            return services;
        }
    }
}
