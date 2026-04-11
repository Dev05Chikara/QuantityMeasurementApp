using System;
using System.Text.Json.Serialization;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Data Transfer Object (DTO) for holding quantity measurement input data - value and corresponding unit and its measurement.
    /// </summary>
    public class QuantityDTO
    {
        /// <summary>
        /// The quantity value.
        /// </summary>
        [JsonPropertyName("value")]
        public double Value { get; set; }

        /// <summary>
        /// The unit name (e.g., "FEET", "KILOGRAM").
        /// </summary>
        [JsonPropertyName("unitName")]
        public string UnitName { get; set; } = string.Empty;

        /// <summary>
        /// The measurement type (e.g., "Length", "Weight").
        /// </summary>
        [JsonPropertyName("measurementType")]
        public string MeasurementType { get; set; } = string.Empty;
    }
}


