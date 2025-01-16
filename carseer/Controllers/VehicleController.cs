using System;
using AutoMapper;
using carseer.Models.DTO;
using carseer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using migration_postgress.Response;

namespace carseer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService vehicleService;
        private readonly IMapper mapper;

        public VehicleController(VehicleService vehicleService,IMapper mapper)
        {
            this.vehicleService = vehicleService;
            this.mapper = mapper;
        }
        [HttpGet("makes")]
        public async Task<IActionResult> GetAllMakes()
        {
            try
            {
                var makes = await vehicleService.GetAllMakesAsync();
                var makeDTOs = mapper.Map<List<MakeDTO>>(makes);
                var response = APIResponse<List<MakeDTO>>.SuccessResponse(makeDTOs);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while fetching vehicle makes: {ex.Message}";
                var response = APIResponse<List<MakeDTO>>.ErrorResponse(errorMessage);
                return StatusCode(500, response); 
            }
        }

        [HttpGet("{makeId}/types")]
        public async Task<IActionResult> GetVehicleTypesForMakeId(int makeId)
        {
            try
            {
                var vehicleTypes = await vehicleService.GetVehicleTypesForMakeIdAsync(makeId);
                var vehicleTypeDTOs = mapper.Map<List<VehicleTypeDTO>>(vehicleTypes);
                var response = APIResponse<List<VehicleTypeDTO>>.SuccessResponse(vehicleTypeDTOs);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while fetching vehicle types: {ex.Message}";
                var response = APIResponse<List<VehicleTypeDTO>>.ErrorResponse(errorMessage);
                return StatusCode(500, response);
            }
        }

        [HttpGet("models")]
        public async Task<IActionResult> GetModelsForMakeIdYear([FromQuery] int makeId, [FromQuery] int year, [FromQuery] string vehicleType)
        {
            try
            {
                var models = await vehicleService.GetModelsForMakeIdYearAsync(makeId, year,vehicleType);
                if (models == null || !models.Any())
                {
                    return NotFound(APIResponse<List<ModelDTO>>.ErrorResponse("No models found for the given criteria.", 404));
                }
                var modelDTOs = mapper.Map<List<ModelDTO>>(models);
                var response = APIResponse<List<ModelDTO>>.SuccessResponse(modelDTOs);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while fetching models: {ex.Message}";
                var response = APIResponse<List<ModelDTO>>.ErrorResponse(errorMessage);
                return StatusCode(500, response);
            }
        }
    }
}
