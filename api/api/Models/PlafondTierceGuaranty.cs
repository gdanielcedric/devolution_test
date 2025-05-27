namespace api.Models
{
    public class PlafondTierceGuaranty : Guaranty
    {
        public double Value {  get; set; }
        public override double CalculatePrime()
        {
            // valeur assurée
            double assur_value = Value*0.5;
            // valeur de la prime
            double prime = (assur_value * 4.2) / 100;

            if (prime < 100000) prime = 100000;

            return prime;
        }
    }
}
