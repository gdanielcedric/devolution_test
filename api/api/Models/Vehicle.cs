namespace api.Models
{
    public class Vehicle : BaseEntity
    {
        public DateTime DateFirstCirculation { get; set; }
        public string ImmatriculationNumber { get; set; } = string.Empty;
        public string Couleur { get; set; } = string.Empty;
        public int NombreSiege { get; set; }
        public int NombrePorte { get; set; }
        public string IdCategoryVehicle { get; set; } = string.Empty;
        public string IdSuscriber { get; set; } = string.Empty;
        public double ValueNeuve { get; set; }
        public double ValueVenale { get; set; }
        public int FiscalPower { get; set; }
    }
}
