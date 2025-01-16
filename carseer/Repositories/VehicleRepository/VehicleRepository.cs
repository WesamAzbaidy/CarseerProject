using carseer.Models.Domain;

namespace carseer.Repositories.VehicleRepository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly HttpClient _httpClient;

        public VehicleRepository(HttpClient httpClient) {
            this._httpClient = httpClient;
        }
        public Task<List<Make>> GetAllMakesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Model>> GetModelsForMakeIdYearAsync(int makeId, int year, string vehicleType)
        {
            throw new NotImplementedException();
        }

        public Task<List<VehicleType>> GetVehicleTypesForMakeIdAsync(int makeId)
        {
            throw new NotImplementedException();
        }
    }
}
