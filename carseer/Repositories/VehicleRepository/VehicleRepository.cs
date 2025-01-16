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

        public async Task<List<Model>> GetModelsForMakeIdYearAsync(int makeId, int year, string vehicleType)
        {
            var response = await _httpClient.GetStringAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?vehicletype={vehicleType}&format=json");
            var data = JsonConvert.DeserializeObject<dynamic>(response);
            return JsonConvert.DeserializeObject<List<Model>>(data.Results.ToString());
        }

        public async Task<List<VehicleType>> GetVehicleTypesForMakeIdAsync(int makeId)
        {
            var response = await _httpClient.GetStringAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{makeId}?format=json");
            var data = JsonConvert.DeserializeObject<dynamic>(response);
            return JsonConvert.DeserializeObject<List<VehicleType>>(data.Results.ToString());
        }
    }
}
