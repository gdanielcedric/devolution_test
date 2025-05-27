namespace api.Models
{
    public class RCGuaranty : Guaranty
    {
        public int FiscalPower {  get; set; }
        public override double CalculatePrime()
        {
            double amount = FiscalPower switch
            {
                2 => 37601,
                > 2 and < 7 => 45181,
                >= 7 and <= 10 => 51078,
                >= 11 and <= 14 => 65677,
                >= 15 and <= 23 => 86456,
                >= 24 => 104143,
                _ => 0
            };

            return amount;
        }
    }
}
