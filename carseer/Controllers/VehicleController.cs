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
        public async Task<IActionResult> GetAllMakes([FromQuery] int size = 10, [FromQuery] string makeNameSearch = null)
        {
            try
            {
                var makes = await vehicleService.GetAllMakesAsync(size, makeNameSearch);
                var makeDTOs = mapper.Map<List<MakeDTO>>(makes);

                var response = new
                {
                    size = size,
                    TotalItems = makeDTOs.Count,
                    Data = makeDTOs
                };

                return Ok(APIResponse<object>.SuccessResponse(response));
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while fetching vehicle makes: {ex.Message}";
                var response = APIResponse<object>.ErrorResponse(errorMessage);
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
        public async Task<IActionResult> GetModelsForMakeIdYear(
     [FromQuery] int makeId,
     [FromQuery] int year,
     [FromQuery] string vehicleType,
     [FromQuery] int pageNumber = 1,
     [FromQuery] int pageSize = 5)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest(APIResponse<string>.ErrorResponse("Page number and page size must be greater than zero.", 400));
                }

                var (models, totalCount) = await vehicleService.GetModelsForMakeIdYearAsync(makeId, year, vehicleType, pageNumber, pageSize);

                if (models == null || !models.Any())
                {
                    return NotFound(APIResponse<object>.ErrorResponse("No models found for the given criteria.", 404));
                }

                var modelDTOs = mapper.Map<List<ModelDTO>>(models);

                var response = new
                {
                    TotalCount = totalCount,
                    NumberOfItems = modelDTOs.Count,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                    Data = modelDTOs
                };

                return Ok(APIResponse<object>.SuccessResponse(response));
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while fetching models: {ex.Message}";
                var response = APIResponse<string>.ErrorResponse(errorMessage);
                return StatusCode(500, response);
            }
        }


    }
}
