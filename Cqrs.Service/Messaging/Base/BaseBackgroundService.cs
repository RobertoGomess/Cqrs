using Cqrs.Infra.CrossCutting.Configuration;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Service.Messaging.Base
{
    public abstract class BaseBackgroundService<Message>: BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BaseBackgroundService<Mediator>> _logger;
        private IOptions<RabbitMQSettings> _config;
        private readonly string _queueName;
        private IConnection _connection;
        private IModel _channel;

        protected BaseBackgroundService(IMediator mediator, string queueName,IOptions<RabbitMQSettings> config, ILogger<BaseBackgroundService<Mediator>> logger)
        {
            _mediator = mediator;
            _queueName = queueName;
            _config = config;
            _logger = logger;
            InitializeListener();
        }

        protected virtual void InitializeListener()
        {
            if (_config.Value.Active)
            {
                _logger.LogInformation("Start InitializeListener");

                _connection = BaseConnectionFactory.GetInstance(_config).CreateConnection();
                _connection.ConnectionShutdown += ConnectionShutdown;
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                _logger.LogInformation("End InitializeListener");
            }
            else
            {
                _logger.LogInformation("Messaging: Off");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonConvert.DeserializeObject<Message>(content);

                HandleMessage(message);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        protected async virtual void HandleMessage(Message message)
        {
            _logger.LogInformation("Start HandleMessage");
            await _mediator.Send(message);
            _logger.LogInformation("End HandleMessage");
        }

        protected virtual void ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("ConnectionShutdown");
        }

        protected virtual void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("OnConsumerShutdown");
        }

        protected virtual void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation("OnConsumerCancelled");
        }

        protected virtual void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation("OnConsumerUnregistered");
        }

        protected virtual void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation("OnConsumerRegistered");
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

    }
}
