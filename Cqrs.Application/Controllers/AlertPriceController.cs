using Cqrs.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Cqrs.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertPriceController : ControllerBase
    {
        private readonly ILogger<AlertPriceController> _log;
        private readonly IMediator _mediator;

        public AlertPriceController(ILogger<AlertPriceController> logger, IMediator mediator)
        {
            _log = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Get alert prices.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /AlertPrice
        ///     {
        ///        "Recents": true
        ///     }
        ///
        /// </remarks>
        /// <param name="getAlertPriceQuery"></param>
        /// <returns>Prices alert list</returns>
        /// <response code="200">Returns the prices alert list</response>
        /// <response code="400">If the getAlertPriceQuery is null</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery]GetAlertPriceQueryRequest getAlertPriceQuery)
        {
            _log.LogInformation("Get Alert price");

            return await Task.Run(() => Ok( _mediator.Send(getAlertPriceQuery).Result));
        }
    }
}
