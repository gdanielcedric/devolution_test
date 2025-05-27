namespace api.Models
{
    public class VolGuaranty : Guaranty
    {
        public double Value {  get; set; }
        public override double CalculatePrime()
        {
            return (0.14*Value)/100;
        }
    }
}
