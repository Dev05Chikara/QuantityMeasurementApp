using System;

namespace QuantityMeasurementApp.QuantityMeasurementModel
{
    /// <summary>
    /// Data Transfer Object (DTO) for holding quantity measurement input data - value and corresponding unit and its measurement.
    /// </summary>
    public class QuantityDTO
    {
        /// <summary>
        /// The quantity value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The unit name (e.g., "FEET", "KILOGRAM").
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// The measurement type (e.g., "Length", "Weight").
        /// </summary>
        public string MeasurementType { get; set; }

        /// <summary>
        /// Interface defined within this DTO class to represent measurable units for quantity measurements.
        /// This is different from the IMeasurable interface defined in the application.
        /// </summary>
        public interface IMeasurableUnit
        {
            double GetConversionFactor();
            double ConvertToBaseUnit(double value);
            double ConvertFromBaseUnit(double baseValue);
            string GetUnitName();
            string GetMeasurementType();
        }

        // In a full implementation, the enums LengthUnit, VolumeUnit, etc., would be defined here
        // to provide a self-contained representation. For this refactoring, we use the existing units.
    }
}