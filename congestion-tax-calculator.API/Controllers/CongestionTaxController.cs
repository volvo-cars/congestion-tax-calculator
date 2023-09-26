using congestion_tax_calculator.Application.CQRS.Queries.CongestionTax;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace congestion_tax_calculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongestionTaxController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CongestionTaxController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("calculate")]
        [SwaggerOperation(
      Summary = "Calculate Congestion Tax",
      Description = "Calculate Congestion Tax by Passage Number Plate",
      OperationId = "CongestionTax.Calculate",
      Tags = new[] { "CongestionTaxController" })]
        public async Task<IActionResult> CalculateCongestionTax([FromBody] GetCongestionTaxQuery query)
        {
            if (query.PassageTimes == null || query.PassageTimes.Count == 0)
            {
                return BadRequest("Passage times are required.");
            }

            var result = await _mediator.Send(query);

            return Ok(new GetCongestionTaxResult { TaxAmount = result });
        }

    }
}
