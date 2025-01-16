using carseer.Models.Domain;
using carseer.Repositories.VehicleRepository;

namespace carseer.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this._vehicleRepository = vehicleRepository;
        }

        public Task<List<Make>> GetAllMakesAsync() => _vehicleRepository.GetAllMakesAsync();

        public Task<List<VehicleType>> GetVehicleTypesForMakeIdAsync(int makeId) => _vehicleRepository.GetVehicleTypesForMakeIdAsync(makeId);

        public Task<List<Model>> GetModelsForMakeIdYearAsync(int makeId, int year, string vehicleType) => _vehicleRepository.GetModelsForMakeIdYearAsync(makeId, year, vehicleType);

    }
}
