using CubeGame.DAL.Data.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using CubeGame.Data.Context;
using Azure.Core;
using CubeGame.Data.Models.Account;

namespace CubeGame.DAL.Repo.Service
{
    public class StripeService : IStripeService
    {
        private readonly TokenService _tokenService;
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;
        private readonly ApplicationDBContext _context;


        public StripeService(TokenService tokenService, CustomerService customerService, ChargeService chargeService, ApplicationDBContext context)
        {
            this._tokenService = tokenService;
            this._customerService = customerService;
            this._chargeService = chargeService;
            this._context = context;
        }
        public async Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                //Currency = resource.Currency,
                //Amount = resource.Amount,
                ReceiptEmail = resource.ReceiptEmail,
                Customer = resource.CustomerId
                //Description = resource.Description
            };
            var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);
            return new ChargeResource(
            charge.Id,
            //charge.Currency,
            //charge.Amount,
            charge.CustomerId,
            charge.ReceiptEmail
            /*charge.Description*/);

        }

        public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken, string tokenn)
        {
            
            ApplicationUser? account = _context.Users.FirstOrDefault(a => a.token == tokenn);

            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = resource.Card.Name,
                    Number = resource.Card.Number,
                    ExpYear = resource.Card.ExpiryYear,
                    ExpMonth = resource.Card.ExpiryMonth,
                    Cvc = resource.Card.Cvc
                }
            };
            var token = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);

            var customerOptions = new CustomerCreateOptions
            {
                Email = account.Email,
                Name = account.UserName,
                Source = token.Id
            };
            var customer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);

            return new CustomerResource(customer.Id, customer.Email, customer.Name);
        }
    }
}
