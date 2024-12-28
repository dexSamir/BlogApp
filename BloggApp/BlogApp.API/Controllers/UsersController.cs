using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BlogApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service; 
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _service.Register(dto);
            return Ok("Successfully registered!");
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            await _service.Login(dto); 
            return Ok($"Welcome {dto.Username}!");
        }

    }
}
