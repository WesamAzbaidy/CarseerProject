using carseer.Models.Domain;
using carseer.Repositories.VehicleRepository;
using Microsoft.Extensions.Caching.Memory;

namespace carseer.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMemoryCache memoryCache;

        public VehicleService(IVehicleRepository vehicleRepository,IMemoryCache memoryCache)
        {
            this._vehicleRepository = vehicleRepository;
            this.memoryCache = memoryCache;
        }

        public async Task<List<Make>> GetAllMakesAsync(int page, int pageSize, string makeNameSearch = null)
        {
            // Check if data is already in memory cache
            if (!memoryCache.TryGetValue("allMakes", out List<Make> allMakes))
            {
                // If not, fetch data from the external API
                allMakes = await _vehicleRepository.GetAllMakesAsync();

                // Cache the data in memory for a certain period (e.g., 1 hour)
                var cacheExpirationOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                memoryCache.Set("allMakes", allMakes, cacheExpirationOptions);
            }

            // Apply search filter if makeNameSearch is provided
            if (!string.IsNullOrEmpty(makeNameSearch))
            {
                allMakes = allMakes.Where(m => m.Make_Name.Contains(makeNameSearch, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply pagination
            var paginatedMakes = allMakes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return paginatedMakes;
        }

        public Task<List<VehicleType>> GetVehicleTypesForMakeIdAsync(int makeId) => _vehicleRepository.GetVehicleTypesForMakeIdAsync(makeId);

        public Task<List<Model>> GetModelsForMakeIdYearAsync(int makeId, int year,string vehicleType) => _vehicleRepository.GetModelsForMakeIdYearAsync(makeId, year, vehicleType);


    }
}
