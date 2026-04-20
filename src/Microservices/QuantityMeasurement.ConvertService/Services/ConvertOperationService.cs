using QuantityMeasurement.SharedKernel.Core.Exceptions;
using QuantityMeasurement.SharedKernel.Core.Quantities;
using QuantityMeasurement.SharedKernel.Core.Units;
using QuantityMeasurement.SharedKernel.DTOs;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.ConvertService.Services
{
    /// <summary>
    /// Handles the Convert operation: converts a quantity from one unit to another.
    /// </summary>
    public class ConvertOperationService
    {
        private readonly IMeasurementHistoryRepository _repo;
        public ConvertOperationService(IMeasurementHistoryRepository repo) => _repo = repo;

        public QuantityDTO Convert(QuantityDTO dto, string targetUnitName, string username)
        {
            try
            {
                Validate(dto);
                if (string.IsNullOrEmpty(targetUnitName)) throw new ArgumentException("Target unit name is required");

                QuantityDTO result = dto.MeasurementType switch
                {
                    "Length" => DoConvert<LengthUnit>(dto, targetUnitName),
                    "Weight" => DoConvert<WeightUnit>(dto, targetUnitName),
                    "Volume" => DoConvert<VolumeUnit>(dto, targetUnitName),
                    "Temperature" => DoConvert<TemperatureUnit>(dto, targetUnitName),
                    _ => throw new ArgumentException("Unsupported measurement type")
                };

                SaveHistory(dto, OperationType.CONVERT, result, username);
                return result;
            }
            catch (Exception ex)
            {
                SaveErrorHistory(dto, OperationType.CONVERT, ex.Message, username);
                throw new QuantityMeasurementException("Conversion failed: " + ex.Message, ex);
            }
        }

        private static QuantityDTO DoConvert<U>(QuantityDTO dto, string targetUnitName) where U : Enum
        {
            var sourceUnit = (U)Enum.Parse(typeof(U), dto.UnitName, true);
            var targetUnit = (U)Enum.Parse(typeof(U), targetUnitName, true);
            var source = new Quantity<U>(dto.Value, sourceUnit);
            var converted = source.ConvertTo(targetUnit);
            return new QuantityDTO { Value = converted.Value, UnitName = targetUnitName, MeasurementType = dto.MeasurementType };
        }

        private static void Validate(QuantityDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrEmpty(dto.UnitName)) throw new ArgumentException("Unit name is required");
            if (string.IsNullOrEmpty(dto.MeasurementType)) throw new ArgumentException("Measurement type is required");
        }

        private void SaveHistory(QuantityDTO op, OperationType opType, QuantityDTO result, string user) =>
            _repo.Save(new QuantityMeasurementHistoryRecord
            {
                Username = user, Operation = opType, CreatedAtUtc = DateTime.UtcNow,
                Operand1Value = op.Value, Operand1UnitName = op.UnitName, Operand1MeasurementType = op.MeasurementType,
                ResultValue = result.Value, ResultUnitName = result.UnitName, ResultMeasurementType = result.MeasurementType
            });

        private void SaveErrorHistory(QuantityDTO op, OperationType opType, string errorMsg, string user) =>
            _repo.Save(new QuantityMeasurementHistoryRecord
            {
                Username = user, Operation = opType, CreatedAtUtc = DateTime.UtcNow, ErrorMessage = errorMsg,
                Operand1Value = op.Value, Operand1UnitName = op.UnitName, Operand1MeasurementType = op.MeasurementType
            });
    }
}
