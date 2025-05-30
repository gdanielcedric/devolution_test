namespace api.DTO
{
    public class SimulationDTO
    {
        public required string IdAssurProduct { get; set; }
        public required VehicleDTO Vehicle { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
