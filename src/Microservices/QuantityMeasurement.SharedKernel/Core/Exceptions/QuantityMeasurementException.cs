namespace QuantityMeasurement.SharedKernel.Core.Exceptions
{
    /// <summary>
    /// Domain exception thrown when a quantity measurement operation fails.
    /// </summary>
    public class QuantityMeasurementException : Exception
    {
        public QuantityMeasurementException(string message) : base(message) { }
        public QuantityMeasurementException(string message, Exception inner) : base(message, inner) { }
    }
}
