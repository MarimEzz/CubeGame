using CubeGame.DAL.Repo.cart;
using CubeGame.DAL.Repo.wishlist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using CubeGame.BL.Manager;

namespace CubeGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class WishlistController : ControllerBase
    {
        private readonly IwishlistRepo wishlistRepo;
        private readonly IWishlistManager wishlistManager;

        public WishlistController(IwishlistRepo _wishlistRepo, IWishlistManager _wishlistManager)
        {
            wishlistRepo = _wishlistRepo;
            wishlistManager = _wishlistManager;
        }
        [Authorize]
        [HttpGet("getAll")]
        public IActionResult getAll()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var wishlist = wishlistManager.GetAll(token);

            return Ok(wishlist);

        }
        [Authorize]
        [HttpPost("AddTowishlist/{Id}")]
        public IActionResult AddTowishlist(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            wishlistRepo.AddToWishlist(Id, token);

            return Created("url", Id);
        }
        [Authorize]
        [HttpDelete("clearwishlist")]
        public IActionResult clearwishlist()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            wishlistRepo.ClearWishList(token);
            return Ok();



        }
        [Authorize]
        [HttpDelete("RemoveFromwishlist/{id}")]
        public IActionResult RemoveFromwishlist(int id)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            wishlistRepo.RemoveFromWishList(id, token);

            return Ok("ok");

        }


    }
}

    