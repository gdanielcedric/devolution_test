namespace api.DTO
{
    public class SubscriptionDTO
    {
        public required VehicleDTO Vehicle { get; set; }
        public required SuscriberDTO Suscriber { get; set; }
    }
}
