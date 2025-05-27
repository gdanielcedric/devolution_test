namespace api.Models
{
    public class Suscriber : BaseEntity
    {
        public string Address { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string CNI { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
    }
}
