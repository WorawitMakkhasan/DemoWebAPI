using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApiDemo.Core.IConfiguration;
using WebApiDemo.Model;
using WebApiDemo.Model.Dto;

namespace WebApiDemo.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuraton;
        private readonly ILogger<StudentConrtroller> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IConfiguration configuration, IUnitOfWork unitOfWork, ILogger<StudentConrtroller> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuraton = configuration;
        }


        [HttpPost("register")]
        public async Task<ActionResult<Student>> Register(AuthDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var studentInDB = await _unitOfWork.Student.GetById(request.Id);
            studentInDB.Username = request.Username;
            studentInDB.PasswordHash = passwordHash;
            studentInDB.PasswordSalt = passwordSalt;
            await _unitOfWork.CompleteAsync();

            return (studentInDB);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<Student>> Login(AuthDto request)
        {
            var studentById = await _unitOfWork.Student.GetById(request.Id);

            if (studentById.Username != request.Username)
            {
                return BadRequest("Student not found.");
            }

            if (!VerifyPasswordHash(request.Password, studentById.PasswordHash, studentById.PasswordSalt))
            {
                return BadRequest("Wrong Password.");
            }

            string token = CreatedToken(studentById);
            return Ok(token);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("TokenValidation")]
        public async Task<ActionResult> TokenValidation(AuthDto request)
        {
            var studentById = await _unitOfWork.Student.GetById(request.Id);
            return Ok(studentById);
        }


        private string CreatedToken(Student student)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, student.Name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuraton.GetSection("AppSetting:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuraton.GetSection("AppSetting:Issuer").Value,
                audience: _configuraton.GetSection("AppSetting:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hamc = new HMACSHA512())
            {
                passwordSalt = hamc.Key;
                passwordHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hamc = new HMACSHA512(passwordSalt))
            {
                var computeHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
