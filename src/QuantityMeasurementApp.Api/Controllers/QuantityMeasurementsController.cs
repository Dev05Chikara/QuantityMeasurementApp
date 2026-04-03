using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuantityMeasurementsController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementsController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        [HttpPost("compare")]
        public ActionResult<QuantityDTO> Compare([FromBody] QuantityPairRequestDto request)
        {
            var result = _service.Compare(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [HttpPost("convert")]
        public ActionResult<QuantityDTO> Convert([FromBody] QuantityConvertRequestDto request)
        {
            var result = _service.Convert(request.Quantity, request.TargetUnitName);
            return Ok(result);
        }

        [HttpPost("add")]
        public ActionResult<QuantityDTO> Add([FromBody] QuantityPairRequestDto request)
        {
            var result = _service.Add(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [HttpPost("subtract")]
        public ActionResult<QuantityDTO> Subtract([FromBody] QuantityPairRequestDto request)
        {
            var result = _service.Subtract(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [HttpPost("divide")]
        public ActionResult<QuantityDTO> Divide([FromBody] QuantityPairRequestDto request)
        {
            var result = _service.Divide(request.Operand1, request.Operand2);
            return Ok(result);
        }

        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery] OperationType? operationType)
        {
            var history = _service.GetOperationHistory(operationType);
            return Ok(history);
        }
    }
}



