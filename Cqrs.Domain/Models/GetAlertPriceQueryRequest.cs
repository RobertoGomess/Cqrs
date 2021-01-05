using MediatR;
using System.Collections.Generic;

namespace Cqrs.Domain.Models
{
    public class GetAlertPriceQueryRequest : IRequest<IEnumerable<AlertPrice>>
    {
        public bool Recents { get; set; }
    }
}
