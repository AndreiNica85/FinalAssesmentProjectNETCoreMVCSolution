using FinalAssesmentProjectNETCoreMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAssesmentProjectNETCoreMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly AndreiNicaDbContext _context;

        public OwnersController(AndreiNicaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Owner>> Get()
        {
            return await _context.Owners.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
                return null;
            return Ok(owner);

        }

        [HttpPost]
        public async Task<OkResult> Post(Owner owner)
        {
            _context.Add(owner);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<NotFoundResult> Put(Owner ownerData)
        {
            if (ownerData == null || ownerData.Id == 0)
                return null;

            var owner = await _context.Owners.FindAsync(ownerData.Id);
            if (owner == null)
                return NotFound();
            owner.FirstName = ownerData.FirstName;
            owner.LastName = ownerData.LastName;
            owner.DriverLicense = ownerData.DriverLicense;
            await _context.SaveChangesAsync();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null) return null;
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
