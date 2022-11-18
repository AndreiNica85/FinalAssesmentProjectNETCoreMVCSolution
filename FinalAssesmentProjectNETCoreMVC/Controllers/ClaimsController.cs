using FinalAssesmentProjectNETCoreMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAssesmentProjectNETCoreMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {

        private readonly AndreiNicaDbContext _context;

        [HttpGet]
        public async Task<List<Claim>> Get()
        {
            return await _context.Claims.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var claim = await _context.Claims.FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
                return null;
            return Ok(claim);

        }

        [HttpPost]
        public async Task<OkResult> Post(Claim claim)
        {
            _context.Add(claim);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<NotFoundResult> Put(Claim claimData)
        {
            if (claimData == null || claimData.Id == 0)
                return null;

            var claim = await _context.Claims.FindAsync(claimData.Id);
            if (claim == null)
                return NotFound();
            claim.Description = claimData.Description;
            claim.Status = claimData.Status;
            claim.Date = claimData.Date;
            claim.VehicleId = claimData.VehicleId;
   
            await _context.SaveChangesAsync();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return null;
            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
