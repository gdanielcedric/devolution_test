namespace api.DTO
{
    public class VSSDTO
    {
        public string Id { get; set; } = string.Empty;
        public string QuoteReference { get; set; } = string.Empty;
        public string ImmatriculationNumber { get; set; } = string.Empty;
        public string AssurProduct { get; set; } = string.Empty;
        public double Price { get; set; }
        public string SuscriberNom { get; set; } = string.Empty;
        public string SuscriberPhone { get; set; } = string.Empty;
        public string Step { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
