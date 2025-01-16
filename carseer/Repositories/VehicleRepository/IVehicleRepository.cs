using carseer.Models.Domain;

namespace carseer.Repositories.VehicleRepository
{
    public interface IVehicleRepository
    {
        Task<List<Make>> GetAllMakesAsync();
        Task<List<VehicleType>> GetVehicleTypesForMakeIdAsync(int makeId);
        Task<List<Model>> GetModelsForMakeIdYearAsync(int makeId, int year, string vehicleType);
    }
}
