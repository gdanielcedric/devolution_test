using api.DTO;
using api.DTO.Filters;
using api.Models;

namespace api.Interfaces
{
    public interface ISimulationServices
    {
        public PagedResult<Simulation> getAll(SearchEntityDto<SimulationFilter> search, string? id = "");
        public Task<Simulation> getDetail(string id);
        public Task<Simulation> createSimulation(Simulation simulation);
    }
}
