using team_stranger_strings_backend.Models;
using team_stranger_strings_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace team_stranger_strings_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase 
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleRepository _VehicleRepository;

        public VehicleController(ILogger<VehicleController> logger, IVehicleRepository repository)
        {
            _logger = logger;
            _VehicleRepository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetVehicle() 
        {
            return Ok(_VehicleRepository.GetAllVehicles());
        }

        [HttpGet]
        [Route("{VehicleId:int}")]
        public ActionResult<Vehicle> GetVehicleById(int VehicleId) 
        {
            var Vehicle = _VehicleRepository.GetVehicleById(VehicleId);
            if (Vehicle == null) {
                return NotFound();
            }
            return Ok(Vehicle);
        }

        [HttpPost]
        public ActionResult<Vehicle> CreateVehicle(Vehicle Vehicle) 
        {
            if (!ModelState.IsValid || Vehicle == null) {
                return BadRequest();
            }
            var newVehicle = _VehicleRepository.CreateVehicle(Vehicle);
            return Created(nameof(GetVehicleById), newVehicle);
        }

        [HttpPut]
        [Route("{VehicleId:int}")]
        public ActionResult<Vehicle> UpdateVehicle(Vehicle Vehicle) 
        {
            if (!ModelState.IsValid || Vehicle == null) {
                return BadRequest();
            }
            return Ok(_VehicleRepository.UpdateVehicle(Vehicle));
        }

        [HttpDelete]
        [Route("{VehicleId:int}")]
        public ActionResult DeleteVehicle(int VehicleId) 
        {
            _VehicleRepository.DeleteVehicleById(VehicleId); 
            return NoContent();
        }
    }
}