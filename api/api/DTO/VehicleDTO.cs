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
    }
}
