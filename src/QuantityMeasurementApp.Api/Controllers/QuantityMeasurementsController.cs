using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantityMeasurementsController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementsController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        private string GetCurrentUsername()
        {
            // DEBUG: Log all claims
            var claims = User.Claims.ToList();
            Console.WriteLine($"[GetCurrentUsername] Claims count: {claims.Count}");
            foreach (var claim in claims)
            {
                Console.WriteLine($"[GetCurrentUsername] Claim: {claim.Type} = {claim.Value}");
            }
            
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"[GetCurrentUsername] NameIdentifier claim value: {username}");
            
            if (string.IsNullOrEmpty(username))
            {
                // Fallback: try ClaimTypes.Name
                username = User.FindFirst(ClaimTypes.Name)?.Value;
                Console.WriteLine($"[GetCurrentUsername] Fallback to Name claim: {username}");
            }
            
            return username ?? "Anonymous";
        }

        [AllowAnonymous]
        [HttpPost("compare")]
        public ActionResult<QuantityDTO> Compare([FromBody] QuantityPairRequestDto request)
        {
            _service.SetCurrentUsername(GetCurrentUsername());
            var result = _service.Compare(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("convert")]
        public ActionResult<QuantityDTO> Convert([FromBody] QuantityConvertRequestDto request)
        {
            _service.SetCurrentUsername(GetCurrentUsername());
            var result = _service.Convert(request.Quantity, request.TargetUnitName);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("add")]
        public ActionResult<QuantityDTO> Add([FromBody] QuantityPairRequestDto request)
        {
            _service.SetCurrentUsername(GetCurrentUsername());
            var result = _service.Add(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("subtract")]
        public ActionResult<QuantityDTO> Subtract([FromBody] QuantityPairRequestDto request)
        {
            _service.SetCurrentUsername(GetCurrentUsername());
            var result = _service.Subtract(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("divide")]
        public ActionResult<QuantityDTO> Divide([FromBody] QuantityPairRequestDto request)
        {
            _service.SetCurrentUsername(GetCurrentUsername());
            var result = _service.Divide(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery] OperationType? operationType)
        {
            try
            {
                Console.WriteLine($"\n[GetHistory] ==== REQUEST START ====");
                Console.WriteLine($"[GetHistory] Request path: {Request.Path}");
                Console.WriteLine($"[GetHistory] Authorization header present: {Request.Headers.ContainsKey("Authorization")}");
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    Console.WriteLine($"[GetHistory] Authorization value: {Request.Headers["Authorization"].ToString().Substring(0, 20)}...");
                }
                
                var username = GetCurrentUsername();
                Console.WriteLine($"[GetHistory] Extracted Username: {username}");
                Console.WriteLine($"[GetHistory] Operation Filter: {operationType}");
                
                _service.SetCurrentUsername(username);
                var history = _service.GetOperationHistoryFlattened(operationType);
                
                Console.WriteLine($"[GetHistory] Records returned: {history.Count}");
                
                foreach (var record in history.Take(3))
                {
                    Console.WriteLine($"[GetHistory] Record {record.Id}: {record.Operation}");
                }
                
                Console.WriteLine($"[GetHistory] ==== RETURNING OK ====\n");
                return Ok(history);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetHistory] ERROR: {ex.Message}");
                Console.WriteLine($"[GetHistory] Stack: {ex.StackTrace}");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}



