using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuantityMeasurement.ConvertService.Services;
using QuantityMeasurement.SharedKernel.DTOs;

namespace QuantityMeasurement.ConvertService.Controllers
{
    [ApiController]
    [Route("api/convert")]
    public class ConvertController : ControllerBase
    {
        private readonly ConvertOperationService _service;
        public ConvertController(ConvertOperationService service) => _service = service;

        private string CurrentUser() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst(ClaimTypes.Name)?.Value
            ?? "Anonymous";

        /// <summary>Convert a quantity to a different unit.</summary>
        [HttpPost]
        public ActionResult<QuantityDTO> Convert([FromBody] QuantityConvertRequestDto request)
        {
            var result = _service.Convert(request.Quantity, request.TargetUnitName, CurrentUser());
            return Ok(result);
        }
    }
}
