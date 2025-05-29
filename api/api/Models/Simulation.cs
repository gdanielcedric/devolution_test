namespace api.Models
{
    public class Simulation : BaseEntity
    {
        public string IdVehicle { get; set; } = string.Empty;
        public string IdAssurProduct { get; set; } = string.Empty;
        public string QuoteReference { get; set; } = string.Empty;
        public DateTime EndDate => CreatedAt.AddDays(14);
        public double Price { get; set; }
    }
}
