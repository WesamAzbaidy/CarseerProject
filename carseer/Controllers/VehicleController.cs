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
    }
}
