namespace api.DTO
{
    public class VehicleDTO
    {
        public string Couleur { get; set; } = string.Empty;
        public required string IdCategoryVehicle { get; set; }
        public required DateTime DateFirstUse { get; set; }
        public required int SiegeNumber { get; set; }
        public required int PorteNumber { get; set; }
        public required string Immatriculation { get; set; }
        public double ValueNeuve { get; set; }
        public double ValueVenale { get; set; }
        public int FiscalPower { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
