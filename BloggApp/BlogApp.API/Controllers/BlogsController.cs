using BlogApp.BL.DTOs.BlogDtos;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    readonly IBlogService _service;
    public BlogsController(IBlogService service)
    {
        _service = service; 
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] BlogCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
}
