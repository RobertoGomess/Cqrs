using Cqrs.Domain.Interfaces;
using Cqrs.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Infra.CrossCutting.IoC
{
    public static class RepositoryDependency
    {
        public static void AddRepositoryDependency(this IServiceCollection services)
        {
            services.AddTransient<IAlertPriceRepository, AlertPriceRepository>();
        }

    }
}
