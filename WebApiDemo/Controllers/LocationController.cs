using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Model;
using WebApiDemo.Model.Dto;

namespace WebApiDemo.Controllers
{
    [Route("api/Location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly DataContext _context;
        public LocationController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Locations.ToListAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Location>>> GetLocationById(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return BadRequest("Location Not Found");
            }

            return Ok(location);

        }
        [HttpPost]
        public async Task<ActionResult<List<Location>>> AddInvertory(LocationDto request)
        {
            var location = await _context.Locations.FindAsync(request.StudentId);

            if (location != null)
            {
                return BadRequest("Student already has Location");
            }

            var newlocation = new Location
            {
                Address = request.Address,
                Postcode = request.Postcode,
                StudentId = request.StudentId
            };

            _context.Locations.Add(newlocation);
            await _context.SaveChangesAsync();

            return Ok(newlocation);

        }
    }
}
