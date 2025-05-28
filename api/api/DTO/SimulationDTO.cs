namespace api.DTO
{
    public class SimulationDTO
    {
        public required string IdCategoryVehicle { get; set; }
        public required string IdAssurproduct { get; set; } 
        public required int FiscalPower { get; set; }
        public required int VehicleAge { get; set; }
        public required double NeuveValue { get; set; }
        public required double VenalValue { get; set; }
    }
}
