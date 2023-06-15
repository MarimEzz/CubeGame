using CubeGame.BL.Manager;
using CubeGame.DAL.Repo.cart;
using CubeGame.DAL.Repo.order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CubeGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo orderRepo;
        private readonly IOrderManager orderManager;

        public OrderController(IOrderRepo _repo , IOrderManager _orderManager)
        {
            orderRepo = _repo;
            orderManager = _orderManager;
        }


        [Authorize]
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return Ok(orderManager.GetAllOrders(token));
        }

        [Authorize]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            orderRepo.AddOrder(token);
           return Ok();

        }

        [Authorize]
        [HttpGet("GetUserOrder")]
        public IActionResult getOrderForUser()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (orderManager.GetAllOrderFouUser(token) != null)
            {
                return Ok(orderManager.GetAllOrderFouUser(token));
            }
            return NotFound();
        }


        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

           return Ok(orderRepo.user(token));
        }
    }
}
