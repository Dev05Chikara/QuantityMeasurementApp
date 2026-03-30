namespace QuantityMeasurementApp.QuantityMeasurementModel
{
    /// <summary>
    /// Request payload for binary operations.
    /// </summary>
    public class QuantityPairRequestDto
    {
        public QuantityDTO Operand1 { get; set; } = new();

        public QuantityDTO Operand2 { get; set; } = new();
    }
}
