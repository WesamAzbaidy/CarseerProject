using carseer.Models.Domain;
using Newtonsoft.Json;

namespace carseer.Repositories.VehicleRepository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly HttpClient _httpClient;

        public VehicleRepository(HttpClient httpClient) {
            this._httpClient = httpClient;
        }
        public async Task<List<Make>> GetAllMakesAsync()
        {
            var response = await _httpClient.GetStringAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");
            var data = JsonConvert.DeserializeObject<dynamic>(response);
            return JsonConvert.DeserializeObject<List<Make>>(data.Results.ToString());
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
