﻿using carseer.Models.Domain;
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

        public async Task<List<Make>> GetAllMakesAsync(int size, string makeNameSearch = null)
        {
            if (!memoryCache.TryGetValue("allMakes", out List<Make> allMakes))
            {
                allMakes = await _vehicleRepository.GetAllMakesAsync();

                
                var cacheExpirationOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                memoryCache.Set("allMakes", allMakes, cacheExpirationOptions);
            }

            
            if (!string.IsNullOrEmpty(makeNameSearch))
            {
                allMakes = allMakes
                    .Where(m => m.Make_Name.Contains(makeNameSearch, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            
            return allMakes.Take(size).ToList();
        }

        public Task<List<VehicleType>> GetVehicleTypesForMakeIdAsync(int makeId) => _vehicleRepository.GetVehicleTypesForMakeIdAsync(makeId);

        public Task<List<Model>> GetModelsForMakeIdYearAsync(int makeId, int year,string vehicleType) => _vehicleRepository.GetModelsForMakeIdYearAsync(makeId, year, vehicleType);


    }
}
