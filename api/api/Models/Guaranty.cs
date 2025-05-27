namespace api.Models
{
    public abstract class Guaranty: BaseEntity
    {
        public abstract double CalculatePrime();
    }
}
