using Cqrs.Domain.Models;
using Cqrs.Infra.CrossCutting.Configuration;
using Cqrs.Service.Messaging.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cqrs.Service.Messaging.Receive
{
    public class NewAlertPriceReciever : BaseBackgroundService<CreateAlertPriceRequest>
    {
        public NewAlertPriceReciever(IOptions<RabbitMQSettings> config, IMediator mediator, ILogger<BaseBackgroundService<Mediator>> logger) 
            : base(mediator, config.Value.QueueCreateNewAlertPrice, config, logger)
        {
        }
    }
}
