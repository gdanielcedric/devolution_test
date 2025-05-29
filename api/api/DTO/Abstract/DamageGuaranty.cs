namespace api.DTO.Abstract
{
    public class DamageGuaranty : Guaranty
    {
        public double Value { get; set; }
        public override double CalculatePrime()
        {
            return 2.6 * Value / 100;
        }
    }
}
