using BlogApp.BL.DTOs.CategoryDtos;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        readonly ICategoryRepository _repo;
        public CategoriesController(ICategoryRepository repo)
        {
            _repo = repo; 
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.GetAll().ToListAsync());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDto category)
        {
            await _repo.AddAsync(category);
            await _repo.SaveAsync();
            return Ok(); 
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
