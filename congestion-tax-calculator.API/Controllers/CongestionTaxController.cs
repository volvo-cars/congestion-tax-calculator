using congestion_tax_calculator.Application.CQRS.Commands.CongestionTax;
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

        [HttpPost("rule")]
        [SwaggerOperation(
      Summary = "Save a Congestion Tax Rule",
      Description = "Save a Congestion Tax Rule",
      OperationId = "CongestionTaxRule.Add",
      Tags = new[] { "CongestionTaxController" })]
        public async Task<IActionResult> AddCongestionTaxRule([FromBody] SaveCongestionTaxRuleCommand saveCongestionTaxRuleCommand)
        {
            var result = await _mediator.Send(saveCongestionTaxRuleCommand);
            return Ok(result);
        }

        [HttpDelete("Id")]
        [SwaggerOperation(
      Summary = "Delete a Rule",
      Description = "Remove a Rule with id",
      OperationId = "CongestionTaxRule.DeleteById",
      Tags = new[] { "CongestionTaxController" })]
        public async Task<IActionResult> DeleteCongestionTaxRule([FromQuery] DeleteCongestionTaxRuleCommand deleteCongestionTaxRuleCommand)
        {
            return Ok(await _mediator.Send(deleteCongestionTaxRuleCommand));
        }

    }
}
