using QuantityMeasurement.SharedKernel.Core.Exceptions;
using QuantityMeasurement.SharedKernel.Core.Quantities;
using QuantityMeasurement.SharedKernel.Core.Units;
using QuantityMeasurement.SharedKernel.DTOs;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.ArithmeticService.Services
{
    /// <summary>
    /// Handles Add, Subtract, and Divide operations between two quantities.
    /// </summary>
    public class ArithmeticOperationService
    {
        private readonly IMeasurementHistoryRepository _repo;
        public ArithmeticOperationService(IMeasurementHistoryRepository repo) => _repo = repo;

        // ── Public operations ─────────────────────────────────────────────────────

        public QuantityDTO Add(QuantityDTO dto1, QuantityDTO dto2, string username)
            => Execute(dto1, dto2, OperationType.ADD, username, "Addition",
                (q1, q2) =>
                {
                    dynamic sum = q1.Add(q2);
                    return new QuantityDTO { Value = sum.Value, UnitName = dto1.UnitName, MeasurementType = dto1.MeasurementType };
                });

        public QuantityDTO Subtract(QuantityDTO dto1, QuantityDTO dto2, string username)
            => Execute(dto1, dto2, OperationType.SUBTRACT, username, "Subtraction",
                (q1, q2) =>
                {
                    dynamic diff = q1.Subtract(q2);
                    return new QuantityDTO { Value = diff.Value, UnitName = dto1.UnitName, MeasurementType = dto1.MeasurementType };
                });

        public QuantityDTO Divide(QuantityDTO dto1, QuantityDTO dto2, string username)
            => Execute(dto1, dto2, OperationType.DIVIDE, username, "Division",
                (q1, q2) =>
                {
                    double quotient = q1.Divide(q2);
                    return new QuantityDTO { Value = quotient, UnitName = "DIMENSIONLESS", MeasurementType = "Scalar" };
                });

        // ── Template method ───────────────────────────────────────────────────────

        private QuantityDTO Execute(
            QuantityDTO dto1, QuantityDTO dto2,
            OperationType opType, string username, string opName,
            Func<dynamic, dynamic, QuantityDTO> compute)
        {
            try
            {
                Validate(dto1); Validate(dto2);
                if (dto1.MeasurementType != dto2.MeasurementType)
                    throw new QuantityMeasurementException("Cannot perform operation on different measurement types");

                dynamic q1 = CreateQuantity(dto1);
                dynamic q2 = CreateQuantity(dto2);

                var result = compute(q1, q2);
                SaveHistory(dto1, dto2, opType, result, username);
                return result;
            }
            catch (Exception ex)
            {
                SaveErrorHistory(dto1, dto2, opType, ex.Message, username);
                throw new QuantityMeasurementException($"{opName} failed: " + ex.Message, ex);
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────────

        private static void Validate(QuantityDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrEmpty(dto.UnitName)) throw new ArgumentException("Unit name is required");
            if (string.IsNullOrEmpty(dto.MeasurementType)) throw new ArgumentException("Measurement type is required");
        }

        private static dynamic CreateQuantity(QuantityDTO dto) => dto.MeasurementType switch
        {
            "Length"      => new Quantity<LengthUnit>     (dto.Value, Enum.Parse<LengthUnit>     (dto.UnitName, true)),
            "Weight"      => new Quantity<WeightUnit>     (dto.Value, Enum.Parse<WeightUnit>     (dto.UnitName, true)),
            "Volume"      => new Quantity<VolumeUnit>     (dto.Value, Enum.Parse<VolumeUnit>     (dto.UnitName, true)),
            "Temperature" => new Quantity<TemperatureUnit>(dto.Value, Enum.Parse<TemperatureUnit>(dto.UnitName, true)),
            _             => throw new ArgumentException("Unsupported measurement type")
        };

        private void SaveHistory(QuantityDTO op1, QuantityDTO op2, OperationType op, QuantityDTO result, string user) =>
            _repo.Save(new QuantityMeasurementHistoryRecord
            {
                Username = user, Operation = op, CreatedAtUtc = DateTime.UtcNow,
                Operand1Value = op1.Value, Operand1UnitName = op1.UnitName, Operand1MeasurementType = op1.MeasurementType,
                Operand2Value = op2.Value, Operand2UnitName = op2.UnitName, Operand2MeasurementType = op2.MeasurementType,
                ResultValue = result.Value, ResultUnitName = result.UnitName, ResultMeasurementType = result.MeasurementType
            });

        private void SaveErrorHistory(QuantityDTO op1, QuantityDTO op2, OperationType op, string errorMsg, string user) =>
            _repo.Save(new QuantityMeasurementHistoryRecord
            {
                Username = user, Operation = op, CreatedAtUtc = DateTime.UtcNow, ErrorMessage = errorMsg,
                Operand1Value = op1.Value, Operand1UnitName = op1.UnitName, Operand1MeasurementType = op1.MeasurementType,
                Operand2Value = op2?.Value, Operand2UnitName = op2?.UnitName, Operand2MeasurementType = op2?.MeasurementType
            });
    }
}
