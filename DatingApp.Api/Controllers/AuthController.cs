using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.Dtos;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            userForRegister.UserName = userForRegister.UserName.ToLower();

            if(await _repo.UserExists(userForRegister.UserName))
            return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = userForRegister.UserName
            };

            var createdUser = await _repo.Register(userToCreate, userForRegister.Password);
            return StatusCode(201);
        }
    }
}