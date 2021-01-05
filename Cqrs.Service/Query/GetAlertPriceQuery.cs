using Cqrs.Domain.Interfaces;
using Cqrs.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Service.Query
{
    public class GetAlertPriceQuery : IRequestHandler<GetAlertPriceQueryRequest, IEnumerable<AlertPrice>>
    {
        private readonly IAlertPriceRepository _repository;

        public GetAlertPriceQuery(IAlertPriceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AlertPrice>> Handle(GetAlertPriceQueryRequest request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _repository.GetAll());
        }
    }
}
