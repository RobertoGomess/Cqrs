using Cqrs.Domain.Interfaces;
using Cqrs.Domain.Models;
using Cqrs.Infra.CrossCutting.Configuration;
using Cqrs.Service.Messaging.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Cqrs.Service.Messaging.Send
{
    public class NewAlertPriceSender : ISender<AlertPrice>
    {
        private readonly IOptions<RabbitMQSettings> _config;

        public NewAlertPriceSender(IOptions<RabbitMQSettings> config)
        {
            _config = config;
        }

        public void SendMessage(AlertPrice alertPrice)
        {
            try
            {
                using var connection = BaseConnectionFactory.GetInstance(_config).CreateConnection();
                using var channel = connection.CreateModel();

                var body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(alertPrice));
                channel.BasicPublish(exchange: _config.Value.TopicNewAlertPrice,
                                     routingKey: string.Empty,
                                     basicProperties: null,
                                     body: body);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
