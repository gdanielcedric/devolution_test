namespace api.Models
{
    public class Subscription : BaseEntity
    {
        public string IdSubscriber { get; set; } = string.Empty;
        public string IdVehicle { get; set; } = string.Empty;
        public string IdAssurProduct { get; set; } = string.Empty;
        public string QuoteReference { get; set; } = string.Empty;
        public DateTime EndDate => CreatedAt.AddDays(14);
        public double Price { get; set; }
        public string Step { get; set; } = string.Empty;
    }
}
