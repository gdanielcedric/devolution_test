namespace api.DTO.Abstract
{
    public class IncendieGuaranty : Guaranty
    {
        public double Value { get; set; }
        public override double CalculatePrime()
        {
            return 0.15 * Value / 100;
        }
    }
}
