using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Repository.Models;

namespace QuantityMeasurementApp.Repository.Implementations
{
    /// <summary>
    /// SQL-backed repository for quantity measurement operation history.
    /// </summary>
    public class QuantityMeasurementSqlRepository : IQuantityMeasurementRepository
    {
        private readonly string _connectionString;

        public QuantityMeasurementSqlRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Save(QuantityMeasurementEntity entity, string username)
        {
            const string sql = @"
INSERT INTO dbo.QuantityMeasurementHistory
(
    Username,
    Operation,
    Operand1Value, Operand1UnitName, Operand1MeasurementType,
    Operand2Value, Operand2UnitName, Operand2MeasurementType,
    ResultValue, ResultUnitName, ResultMeasurementType,
    ErrorMessage, CreatedAtUtc
)
VALUES
(
    @Username,
    @Operation,
    @Operand1Value, @Operand1UnitName, @Operand1MeasurementType,
    @Operand2Value, @Operand2UnitName, @Operand2MeasurementType,
    @ResultValue, @ResultUnitName, @ResultMeasurementType,
    @ErrorMessage, @CreatedAtUtc
);";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Username", username ?? "Anonymous");
            command.Parameters.AddWithValue("@Operation", entity.Operation.ToString());

            command.Parameters.AddWithValue("@Operand1Value", entity.Operand1.Value);
            command.Parameters.AddWithValue("@Operand1UnitName", entity.Operand1.UnitName);
            command.Parameters.AddWithValue("@Operand1MeasurementType", entity.Operand1.MeasurementType);

            command.Parameters.AddWithValue("@Operand2Value", GetDbValue(entity.Operand2?.Value));
            command.Parameters.AddWithValue("@Operand2UnitName", GetDbValue(entity.Operand2?.UnitName));
            command.Parameters.AddWithValue("@Operand2MeasurementType", GetDbValue(entity.Operand2?.MeasurementType));

            command.Parameters.AddWithValue("@ResultValue", GetDbValue(entity.Result?.Value));
            command.Parameters.AddWithValue("@ResultUnitName", GetDbValue(entity.Result?.UnitName));
            command.Parameters.AddWithValue("@ResultMeasurementType", GetDbValue(entity.Result?.MeasurementType));

            command.Parameters.AddWithValue("@ErrorMessage", GetDbValue(entity.ErrorMessage));
            command.Parameters.AddWithValue("@CreatedAtUtc", entity.Timestamp.ToUniversalTime());

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements(string username, OperationType? operationType = null)
        {
            const string sql = @"
SELECT
    Operation,
    Operand1Value, Operand1UnitName, Operand1MeasurementType,
    Operand2Value, Operand2UnitName, Operand2MeasurementType,
    ResultValue, ResultUnitName, ResultMeasurementType,
    ErrorMessage, CreatedAtUtc
FROM dbo.QuantityMeasurementHistory
WHERE Username = @Username AND (@Operation IS NULL OR Operation = @Operation)
ORDER BY Id DESC;";

            var measurements = new List<QuantityMeasurementEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Username", username ?? "Anonymous");
            command.Parameters.AddWithValue("@Operation", operationType?.ToString() ?? (object)DBNull.Value);
            connection.Open();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var operationName = reader.GetString(reader.GetOrdinal("Operation"));
                if (!Enum.TryParse(operationName, true, out OperationType operation))
                {
                    continue;
                }
                var timestampUtc = reader.GetDateTime(reader.GetOrdinal("CreatedAtUtc"));
                var errorMessage = GetNullableString(reader, "ErrorMessage");

                var operand1 = new QuantityDTO
                {
                    Value = reader.GetDouble(reader.GetOrdinal("Operand1Value")),
                    UnitName = reader.GetString(reader.GetOrdinal("Operand1UnitName")),
                    MeasurementType = reader.GetString(reader.GetOrdinal("Operand1MeasurementType"))
                };

                QuantityDTO? operand2 = null;
                if (!reader.IsDBNull(reader.GetOrdinal("Operand2Value"))
                    && !reader.IsDBNull(reader.GetOrdinal("Operand2UnitName"))
                    && !reader.IsDBNull(reader.GetOrdinal("Operand2MeasurementType")))
                {
                    operand2 = new QuantityDTO
                    {
                        Value = reader.GetDouble(reader.GetOrdinal("Operand2Value")),
                        UnitName = reader.GetString(reader.GetOrdinal("Operand2UnitName")),
                        MeasurementType = reader.GetString(reader.GetOrdinal("Operand2MeasurementType"))
                    };
                }

                QuantityDTO? result = null;
                if (!reader.IsDBNull(reader.GetOrdinal("ResultValue"))
                    && !reader.IsDBNull(reader.GetOrdinal("ResultUnitName"))
                    && !reader.IsDBNull(reader.GetOrdinal("ResultMeasurementType")))
                {
                    result = new QuantityDTO
                    {
                        Value = reader.GetDouble(reader.GetOrdinal("ResultValue")),
                        UnitName = reader.GetString(reader.GetOrdinal("ResultUnitName")),
                        MeasurementType = reader.GetString(reader.GetOrdinal("ResultMeasurementType"))
                    };
                }

                var localTimestamp = DateTime.SpecifyKind(timestampUtc, DateTimeKind.Utc).ToLocalTime();
                QuantityMeasurementEntity entity;
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    entity = operand2 == null
                        ? new QuantityMeasurementEntity(operand1, operation, errorMessage, localTimestamp)
                        : new QuantityMeasurementEntity(operand1, operand2, operation, errorMessage, localTimestamp);
                }
                else
                {
                    entity = operand2 == null
                        ? new QuantityMeasurementEntity(operand1, operation, result, localTimestamp)
                        : new QuantityMeasurementEntity(operand1, operand2, operation, result, localTimestamp);
                }

                measurements.Add(entity);
            }

            return measurements;
        }

        public List<QuantityMeasurementHistoryRecord> GetAllMeasurementsFlattened(string username, OperationType? operationType = null)
        {
            const string sql = @"
SELECT
    Id,
    Operation,
    Operand1Value, Operand1UnitName, Operand1MeasurementType,
    Operand2Value, Operand2UnitName, Operand2MeasurementType,
    ResultValue, ResultUnitName, ResultMeasurementType,
    ErrorMessage, CreatedAtUtc
FROM dbo.QuantityMeasurementHistory
WHERE Username = @Username AND (@Operation IS NULL OR Operation = @Operation)
ORDER BY Id DESC;";

            var records = new List<QuantityMeasurementHistoryRecord>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Username", username ?? "Anonymous");
            command.Parameters.AddWithValue("@Operation", operationType?.ToString() ?? (object)DBNull.Value);
            connection.Open();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var operationName = reader.GetString(reader.GetOrdinal("Operation"));
                if (!Enum.TryParse<OperationType>(operationName, true, out var operation))
                {
                    operation = OperationType.CONVERT; // Default to CONVERT if parsing fails
                }

                var operand1Value = reader.GetDouble(reader.GetOrdinal("Operand1Value"));

                var record = new QuantityMeasurementHistoryRecord
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Operation = operation,
                    Operand1Value = operand1Value,
                    Operand1UnitName = reader.GetString(reader.GetOrdinal("Operand1UnitName")),
                    Operand1MeasurementType = reader.GetString(reader.GetOrdinal("Operand1MeasurementType")),
                    Operand2Value = GetNullableDouble(reader, "Operand2Value"),
                    Operand2UnitName = GetNullableString(reader, "Operand2UnitName"),
                    Operand2MeasurementType = GetNullableString(reader, "Operand2MeasurementType"),
                    ResultValue = GetNullableDouble(reader, "ResultValue"),
                    ResultUnitName = GetNullableString(reader, "ResultUnitName"),
                    ResultMeasurementType = GetNullableString(reader, "ResultMeasurementType"),
                    ErrorMessage = GetNullableString(reader, "ErrorMessage"),
                    CreatedAtUtc = reader.GetDateTime(reader.GetOrdinal("CreatedAtUtc"))
                };

                records.Add(record);
            }

            return records;
        }

        private static object GetDbValue(object? value)
        {
            return value ?? DBNull.Value;
        }

        private static string? GetNullableString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        private static double? GetNullableDouble(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetDouble(ordinal);
        }
    }
}


