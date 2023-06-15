using CubeGame.BL.Manager;
using CubeGame.DAL.Repo.cart;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CubeGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepo cartRepo;
        private readonly ICartManager CM;
        public CartController(ICartRepo _repo , IHttpContextAccessor httpContextAccessor , ICartManager _CM)
        {
            cartRepo = _repo;
            _httpContextAccessor = httpContextAccessor;
            CM = _CM;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllCartItems()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            return Ok(CM.GetAll(token));
        }
        [Authorize]
        [HttpPost("AddToCart/{id}")]   
        public IActionResult AddToCart(int id)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var p = cartRepo.AddToCart(id, token);

            return Created("url", p);
        }
        [Authorize]
        [HttpDelete("RemoveFromCart/{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var p = cartRepo.RemoveFromCart(id , token);

             return Ok(p);          
           
        }
        [Authorize]
        [HttpDelete("ClearCart")]
        public IActionResult ClearCart()
        {

            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            cartRepo.ClearCart(token);

            return Ok();
        }
    }
}
