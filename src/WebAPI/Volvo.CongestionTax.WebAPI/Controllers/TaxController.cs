using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volvo.CongestionTax.Application.Commands;

namespace Volvo.CongestionTax.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ApiControllerBase
    {
        [HttpPost]
        [Route("calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = "Calculate congestion charge", OperationId = "calculate",
            Tags = new[] {"calculate", "calculation", "congestion", "tax", "congestion-tax"})]
        public async Task<CalculateCongestionTaxCommandResult> CalculateAsync(CalculateCongestionTaxCommand command,
            CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }
    }
}