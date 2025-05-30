namespace api.DTO
{
    public class SuscriberDTO
    {
        public required string Address {  get; set; }
        public required string Cni { get; set; }
        public required string Nom { get; set; }
        public string Prenom { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public required string Telephone { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
