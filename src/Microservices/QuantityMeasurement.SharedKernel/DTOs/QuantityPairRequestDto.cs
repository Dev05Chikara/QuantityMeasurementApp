namespace QuantityMeasurement.SharedKernel.DTOs
{
    public class QuantityPairRequestDto
    {
        public QuantityDTO Operand1 { get; set; } = new();
        public QuantityDTO Operand2 { get; set; } = new();
    }
}
