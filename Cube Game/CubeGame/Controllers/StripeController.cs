using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CubeGame.DAL.Repo.Service;
using CubeGame.DAL.Data.Models.Resources;
using CubeGame.Data.Context;

namespace CubeGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        public StripeController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource,
        CancellationToken cancellationToken)
        {
            string tokenn = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var response = await _stripeService.CreateCustomer(resource, cancellationToken, tokenn);
            return Ok(response);
        }

        [HttpPost("charge")]
        public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource, CancellationToken cancellationToken)
        {
            var response = await _stripeService.CreateCharge(resource, cancellationToken);
            return Ok(response);
        }
    }
}
