using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Core.IConfiguration;
using WebApiDemo.Model;
namespace WebApiDemo.Controllers
{

    [Route("api/RepoStudent")]
    [ApiController]

    public class RepoStudentController : ControllerBase
    {
        private readonly ILogger<StudentConrtroller> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public RepoStudentController(IUnitOfWork unitOfWork, ILogger<StudentConrtroller> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        [HttpPost("/Repo")]
        public async Task<IActionResult> CreateStudent(Student Newstudent)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Student.Add(Newstudent);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction("GetByRepo", new { Newstudent.Id }, Newstudent);
            }

            return new JsonResult("Somthing went wong") { StatusCode = 500 };
        }


        [HttpGet("/Repo/{id}")]
        public async Task<IActionResult> GetByRepo(int id)
        {
            var student = await _unitOfWork.Student.GetById(id);


            if (student == null)
            {
                return BadRequest("Student Not Found");
            }
            return Ok(student);

        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var student = await _unitOfWork.Student.All();
            return Ok(student);
        }
    }
}
