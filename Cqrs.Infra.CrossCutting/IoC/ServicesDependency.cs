using Cqrs.Domain.Interfaces;
using Cqrs.Domain.Models;
using Cqrs.Service.Command;
using Cqrs.Service.Messaging.Receive;
using Cqrs.Service.Messaging.Send;
using Cqrs.Service.Query;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Infra.CrossCutting.IoC
{
    public static class ServicesDependency
    {
        public static void AddMediatRDependency(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAlertPriceQuery).Assembly);
            services.AddMediatR(typeof(CreateAlertPriceCommand).Assembly);
        }

        public static void AddSenderDependency(this IServiceCollection services)
        {
            services.AddSingleton<ISender<AlertPrice>, NewAlertPriceSender>();
        }

        public static void AddRecieverDependency(this IServiceCollection services)
        {
            services.AddHostedService<NewAlertPriceReciever>();
        }
    }
}
