using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Core.IConfiguration;
using WebApiDemo.Model;
using WebApiDemo.Model.Dto;

namespace WebApiDemo.Controllers
{
    [Route("api/Student")]
    [ApiController]


    public class StudentConrtroller : ControllerBase
    {
        private readonly ILogger<StudentConrtroller> _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly DataContext _context;

        public StudentConrtroller(IUnitOfWork unitOfWork, ILogger<StudentConrtroller> logger, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Students
                .Include(s => s.Location)
                .Include(s => s.Invertory)
                .Include(s => s.Courses)
                .ToListAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Location)
                .Include(s => s.Invertory)
                .Include(S => S.Courses)
                .ToListAsync();

            if (student == null)
            {
                return BadRequest("Student Not Found");
            }
            return Ok(student);

        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(Student Newstudent)
        {
            _context.Students.Add(Newstudent);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student Updatestudent)
        {
            var student = await _context.Students.FindAsync(Updatestudent.Id);
            if (student == null)
            {
                return BadRequest("Student Not Found");
            }
            student.Name = Updatestudent.Name;
            student.Age = Updatestudent.Age;

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return BadRequest("Student Not Found");
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }


        [HttpPost("/addStudentCourse")]
        public async Task<ActionResult<List<Student>>> AddStudentCourse(StudentCourseDto NewstudentCourse)
        {
            var student = await _context.Students
                .Where(s => s.Id == NewstudentCourse.studentId)
                .Include(c => c.Courses)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return BadRequest("Student Not Found");
            }

            var cousrse = await _context.Courses.FindAsync(NewstudentCourse.CourseId);

            if (cousrse == null)
            {
                return BadRequest("Course Not Found");
            }
            student.Courses.Add(cousrse);
            await _context.SaveChangesAsync();
            return Ok(student);
        }
    }
}

