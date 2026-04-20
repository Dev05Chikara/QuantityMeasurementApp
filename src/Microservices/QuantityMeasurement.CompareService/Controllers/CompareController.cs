using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuantityMeasurement.CompareService.Services;
using QuantityMeasurement.SharedKernel.DTOs;

namespace QuantityMeasurement.CompareService.Controllers
{
    [ApiController]
    [Route("api/compare")]
    public class CompareController : ControllerBase
    {
        private readonly CompareOperationService _service;
        public CompareController(CompareOperationService service) => _service = service;

        private string CurrentUser() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst(ClaimTypes.Name)?.Value
            ?? "Anonymous";

        /// <summary>Compare two quantities. Returns 1 if equal, 0 if not equal.</summary>
        [HttpPost]
        public ActionResult<QuantityDTO> Compare([FromBody] QuantityPairRequestDto request)
        {
            var result = _service.Compare(request.Operand1, request.Operand2, CurrentUser());
            return Ok(result);
        }
    }
}
