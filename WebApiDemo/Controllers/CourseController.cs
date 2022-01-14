using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Model;
using WebApiDemo.Model.Dto;

namespace WebApiDemo.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DataContext _context;
        public CourseController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Courses.ToListAsync());

        }

        [HttpPost]
        public async Task<ActionResult<List<Course>>> AddInvertory(CourseDto request)
        {
            var course = await _context.Students.FindAsync(request.coursename);

            if (course == null)
            {
                return BadRequest("This course is already have");
            }

            var newcourse = new Course
            {
                coursename = request.coursename,
                credit = request.credit
            };

            _context.Courses.Add(newcourse);
            await _context.SaveChangesAsync();

            return Ok(newcourse);

        }
    }
}
