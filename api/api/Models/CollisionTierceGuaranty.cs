namespace api.Models
{
    public class CollisionTierceGuaranty : Guaranty
    {
        public double Value {  get; set; }
        public override double CalculatePrime()
        {
            return (1.65*Value)/100;
        }
    }
}
