using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BlogApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service; 
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _service.RegisterAsync(dto); 
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }
    [HttpPost]
    public async Task<IActionResult> SendVereficationEmail(string email)
    {
        return Ok(await _service.SendVereficationEmailAsync(email));
    }
    [HttpPost]
    public async Task<IActionResult> VerifyEmail(string email, int code)
    {
        return Ok(await _service.VerifyAccountAsync(email, code));
    }
    [HttpGet("{number}")]
    public async Task<IActionResult> Bitwise(int number)
    {
        int a = 32 | 4 | 2 | 1; 
        return Ok((a & number) == number); 
    }
}
