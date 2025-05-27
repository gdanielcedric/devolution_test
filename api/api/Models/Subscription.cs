namespace api.Models
{
    public class Subscription : BaseEntity
    {
        public string IdSubscriber { get; set; } = string.Empty;
        public string IdVehicle { get; set; } = string.Empty;
        public string IdAssurProduct { get; set; } = string.Empty;
        public string quoteReference {  get; set; } = string.Empty; 
        public DateTime endDate => CreatedAt.AddDays(14);
        public string Step {  get; set; } = string.Empty;
    }
}
