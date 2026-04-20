using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuantityMeasurement.ArithmeticService.Services;
using QuantityMeasurement.SharedKernel.DTOs;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.ArithmeticService.Controllers
{
    [ApiController]
    [Route("api/arithmetic")]
    public class ArithmeticController : ControllerBase
    {
        private readonly ArithmeticOperationService _service;
        private readonly IMeasurementHistoryRepository _historyRepo;

        public ArithmeticController(ArithmeticOperationService service, IMeasurementHistoryRepository historyRepo)
        {
            _service = service;
            _historyRepo = historyRepo;
        }

        private string CurrentUser() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst(ClaimTypes.Name)?.Value
            ?? "Anonymous";

        /// <summary>Add two quantities together.</summary>
        [HttpPost("add")]
        public ActionResult<QuantityDTO> Add([FromBody] QuantityPairRequestDto request)
            => Ok(_service.Add(request.Operand1, request.Operand2, CurrentUser()));

        /// <summary>Subtract second quantity from the first.</summary>
        [HttpPost("subtract")]
        public ActionResult<QuantityDTO> Subtract([FromBody] QuantityPairRequestDto request)
            => Ok(_service.Subtract(request.Operand1, request.Operand2, CurrentUser()));

        /// <summary>Divide first quantity by the second, returning a dimensionless ratio.</summary>
        [HttpPost("divide")]
        public ActionResult<QuantityDTO> Divide([FromBody] QuantityPairRequestDto request)
            => Ok(_service.Divide(request.Operand1, request.Operand2, CurrentUser()));

        /// <summary>Get operation history for the authenticated user.</summary>
        [Authorize]
        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery] OperationType? operationType)
        {
            try
            {
                var username = CurrentUser();
                var history = _historyRepo.GetByUsername(username, operationType);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
