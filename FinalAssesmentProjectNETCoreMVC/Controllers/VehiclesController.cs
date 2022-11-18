using FinalAssesmentProjectNETCoreMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAssesmentProjectNETCoreMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly AndreiNicaDbContext _context;

        [HttpGet]
        public async Task<List<Vehicle>> Get()
        {
            return await _context.Vehicles.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
                return null;
            return Ok(vehicle);

        }

        [HttpPost]
        public async Task<OkResult> Post(Vehicle vehicle)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<NotFoundResult> Put(Vehicle vehicleData)
        {
            if (vehicleData == null || vehicleData.Id == 0)
                return null;

            var vehicle = await _context.Vehicles.FindAsync(vehicleData.Id);
            if (vehicle == null)
                return NotFound();
            vehicle.Brand = vehicleData.Brand;
            vehicle.Vin = vehicleData.Vin;
            vehicle.Color = vehicleData.Color;
            vehicle.Year = vehicleData.Year;
            vehicle.OwnerId = vehicleData.OwnerId;
       
            await _context.SaveChangesAsync();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return null;
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return Ok();

        }

    }
}
