namespace api.Models
{
    public class AssurProduct : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Guaranty> Garanties { get; set; } = new List<Guaranty>();
        public List<CategoryVehicle> Categories { get; set; } = new List<CategoryVehicle>();
    }
}
