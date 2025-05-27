namespace api.Models
{
    public class CategoryVehicle : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
    }
}
