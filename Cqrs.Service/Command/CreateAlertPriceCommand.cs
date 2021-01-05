using Cqrs.Domain.Interfaces;
using Cqrs.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Service.Command
{

    public class CreateAlertPriceCommand : IRequestHandler<CreateAlertPriceRequest, AlertPrice>
    {
        private readonly ISender<AlertPrice> newAlertPricePublisher;
        private readonly IAlertPriceRepository _repository;

        public CreateAlertPriceCommand(ISender<AlertPrice> newAlertPricePublisher, IAlertPriceRepository repository)
        {
            this.newAlertPricePublisher = newAlertPricePublisher;
            _repository = repository;
        }

        public async Task<AlertPrice> Handle(CreateAlertPriceRequest request, CancellationToken cancellationToken)
        {
            await _repository.Create(request);
            newAlertPricePublisher.SendMessage(request);
            return await Task.Run(() => request);
        }
    }
}
