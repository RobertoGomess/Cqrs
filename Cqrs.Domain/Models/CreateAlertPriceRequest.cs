using MediatR;

namespace Cqrs.Domain.Models
{
    public class CreateAlertPriceRequest : AlertPrice, IRequest<AlertPrice>
    {
    }
}
