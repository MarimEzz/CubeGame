using CubeGame.BL.DTO;
using CubeGame.BL.Manager;
using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CubeGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryManager repo;
        public CategoryController(ICategoryManager _repo)
        {
            repo = _repo;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            if (repo.GetAll().Count() > 0)
            {
                return Ok(repo.GetAll());
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            return Ok(repo.GetById(id));
        }       
    

        [HttpPost]
        public IActionResult AddCategory(CategoryDTO c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.Add(c);
                    return Created("url", c);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryDTO c)
        {
            var uCategory = repo.GetById(id);
            if (uCategory == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(id, c);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var c = repo.GetById(id);
            if (c == null)
            {
                return NotFound();
            }
            else
            {
                repo.Delete(id);
                return Ok(c);
            }
        }
    }
}
