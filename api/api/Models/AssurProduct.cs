namespace api.Models
{
    public class AssurProduct : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Garanties { get; set; } = new List<string>();
        public List<string> Categories { get; set; } = new List<string>();
    }
}
