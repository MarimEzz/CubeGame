using CubeGame.BL.DTO;
using CubeGame.BL.Manager;
using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Repo.product;
using CubeGame.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.IO;
using OS = CubeGame.DAL.Data.Models.OS;

namespace CubeGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductManager repo;
        ApplicationDBContext Context;
        private IWebHostEnvironment env;
        private IHttpContextAccessor httpContextAccessor;
        public ProductController( IProductManager _repo, ApplicationDBContext _Context, IWebHostEnvironment _env, IHttpContextAccessor _httpContextAccessor)
        {
            repo = _repo;
            env = _env;
            httpContextAccessor = _httpContextAccessor;
            Context = _Context; 
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            if (repo.GetAll().Count() > 0)
            {
                return Ok(repo.GetAll());
            }
            return NotFound();
        }      
        [HttpGet("GetProductsByCategory/{categoryId}")]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            var products = repo.GetProductsByCategory(categoryId);
            if (products == null)
            {
                return NotFound();
            }
           else if (products.Count() > 0)
            {
                return Ok(products);
            }
            return NotFound();
        }
        [HttpGet("GetProductsByPrice/{price}")]
        public IActionResult GetProductsByPrice(int price)
        {
            var products = repo.GetProductsByPrice(price);
            if (products == null)
            {
                return NotFound();
            }
            else if (products.Count() > 0)
            {
                return Ok(products);
            }
            return NotFound();
        }
        [HttpGet("GetProductsByPlatform/{platform}")]
        public IActionResult GetProductsByPlatform(OS platform)
        {
            var products = repo.GetProductsByPlatform(platform);
            if (products == null)
            {
                return NotFound();
            }
            else if (products.Count() > 0)
            {
                return Ok(products);
            }
            return NotFound();
        }

        [HttpGet("GetProductsByDeveloperName/{developerName}")]
        public IActionResult GetProductsByDeveloperName(string developerName)
        {
            var products = repo.GetProductsByDeveloperName(developerName);
            if (products == null)
            {
                return NotFound();
            }
            else if (products.Count() > 0)
            {
                return Ok(products);
            }
            return NotFound();
        }

        [HttpGet("ProductsWithoutImages")]
        public IActionResult GetAllProductWithoutImages()
        {
            if (repo.GetAllWithoutImage().Count() > 0)
            {

                return Ok(repo.GetAllWithoutImage());
            }
            return NotFound();
        }

        [HttpGet ("GetAllFreeGames")]
        public IActionResult GetAllFreeGames()
        {
            if (repo.GetAllFreeGames().Count() > 0)
            {
                return Ok(repo.GetAllFreeGames());
            }
            return NotFound();
        }
        [HttpGet ("GetAllGameOnSale")]
        public IActionResult GetAllGameOnSale()
        {
            if (repo.GetAllGameOnSale().Count() > 0)
            {
                return Ok(repo.GetAllGameOnSale());
            }
            return NotFound();
        }
        [HttpGet ("GetAllComingSoon")]
        public IActionResult GetAllComingSoon()
        {
            if (repo.GetAllComingSoon().Count() > 0)
            {
                return Ok(repo.GetAllComingSoon());
            }
            return NotFound();
        }
        [HttpGet ("GetAllTopSeller")]
        public IActionResult GetAllTopSeller()
        {
            if (repo.GetAllTopSeller().Count() > 0)
            {
                return Ok(repo.GetAllTopSeller());
            }
            return NotFound();
        }
        [HttpGet ("GetAllRecentlyUpdated")]
        public IActionResult GetAllRecentlyUpdated()
        {
            if (repo.GetAllRecentlyUpdated().Count() > 0)
            {
                return Ok(repo.GetAllRecentlyUpdated());
            }
            return NotFound();
        }
        [HttpGet ("GetAllMostPopular")]
        public IActionResult GetAllMostPopular()
        {
            if (repo.GetAllMostPopular().Count() > 0)
            {
                return Ok(repo.GetAllMostPopular());
            }
            return NotFound();
        }
        [HttpGet ("GetAllMostPlayed")]
        public IActionResult GetAllMostPlayed()
        {
            if (repo.GetAllMostPlayed().Count() > 0)
            {
                return Ok(repo.GetAllMostPlayed());
            }
            return NotFound();
        }
        [HttpGet ("GetAllNewRelease")]
        public IActionResult GetAllNewRelease()
        {
            if (repo.GetAllNewRelease().Count() > 0)
            {
                return Ok(repo.GetAllNewRelease());
            }
            return NotFound();
        }
        [HttpGet ("GetAllTopRated")]
        public IActionResult GetAllTopRated()
        {
            if (repo.GetAllTopRated().Count() > 0)
            {
                return Ok(repo.GetAllTopRated());
            }
            return NotFound();
        }

        [HttpGet("search")]
    
        public IActionResult Search(string SearchItem )
        {
            var products = Context.Products
                .Where(p => p.ProductName.Contains(SearchItem) || p.category.CategoryName.Contains(SearchItem))
                .ToList();

            return Ok(products);
        }

        [HttpGet("ImagesProduct")]
        public IActionResult GetImageProduct(int Productid)
        {
            if (repo.GetProductImages(Productid).Count() > 0)
            {
                return Ok(repo.GetProductImages(Productid));
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            return Ok(repo.getProductByID(id));
        }

        [HttpPost]
        public IActionResult AddProduct(int id, ProductDTO c )
        {          
            if (ModelState.IsValid)
            {
                try
                {                 
                    repo.AddProduct(c);

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
        public IActionResult UpdateProduct(int id, ProductDTO c)
        {
            var uCategory = repo.getProductByID(id);
            if (uCategory == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.EditProduct(id, c);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }
        [HttpDelete("DeleteProduct/{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var c = repo.getProductByID(id);
            if (c == null)
            {
                return NotFound();
            }
            else
            {
                repo.DeleteProduct(id);
                return Ok(c);
            }
        }

        [HttpPost("AddImage/{id:int}")]
        public IActionResult AddImage(int id, IFormFile file)
        {
            Image I = new Image();
            if (ModelState.IsValid)
            {
                try
                {
                    var path = Path.Combine(env.WebRootPath, "uploads", file.FileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    var baseURL = httpContextAccessor.HttpContext.Request.Scheme + "://" +
                        httpContextAccessor.HttpContext.Request.Host +
                        httpContextAccessor.HttpContext.Request.PathBase;

                    var im = baseURL + "/uploads/" + file.FileName;

                    I.ImageURL = im;

                    repo.AddProductImages(id, I);

                    return Created("url", I);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }
    }
}
