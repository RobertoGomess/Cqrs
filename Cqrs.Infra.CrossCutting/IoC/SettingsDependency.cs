using Cqrs.Infra.CrossCutting.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Infra.CrossCutting.IoC
{
    public static class SettingsDependency
    {
        public static void AddSettingsDependency(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<RabbitMQSettings>(Configuration.GetSection("RabbitMQSettings"));
            services.Configure<SqlServerSettings>(Configuration.GetSection("SqlServerSettings"));
        }
    }
}
