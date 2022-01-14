using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Model;
using WebApiDemo.Model.Dto;

namespace WebApiDemo.Controllers
{

    [Route("api/Invertory")]
    [ApiController]
    public class InvertoryController : ControllerBase
    {
        private readonly DataContext _context;
        public InvertoryController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Inventories.ToListAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Inventory>>> GetInvertoryById(int id)
        {
            var Item = await _context.Inventories.FindAsync(id);

            if (Item == null)
            {
                return BadRequest("Item Not Found");
            }

            return Ok(Item);

        }
        [HttpPost]
        public async Task<ActionResult<List<Inventory>>> AddInvertory(InvertoryDto request)
        {
            var Item = await _context.Students.FindAsync(request.StudentId);

            if (Item == null)
            {
                return BadRequest("Student Not Found");
            }

            var newItem = new Inventory
            {
                ItemName = request.ItemName,
                StudentId = request.StudentId
            };

            _context.Inventories.Add(newItem);
            await _context.SaveChangesAsync();

            return Ok(Item);

        }
    }
}
