using Cqrs.Infra.CrossCutting.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Cqrs.Service.Messaging.Base
{
    public class BaseConnectionFactory
    {
        public static ConnectionFactory GetInstance(IOptions<RabbitMQSettings> config)
        {
            return new ConnectionFactory()
            {
                HostName = config.Value.Host,
                Port = config.Value.Port,
                UserName = config.Value.UserName,
                Password = config.Value.Password
            };
        }
    }
}
