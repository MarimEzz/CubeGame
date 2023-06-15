using CubeGame.DAL.Data.Models.Cart;
using CubeGame.Data.Context;
using CubeGame.Data.Models.Account;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.order
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDBContext Context;

        public OrderRepo(ApplicationDBContext _context)
        {
            Context = _context;
        }
        public void AddOrder(string Token)
        {
            Cart? cart = null;

            Order o = null;

            ApplicationUser? UserID = Context.Users.FirstOrDefault(a => a.token == Token);

            if (UserID != null)
            {
                cart = Context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.AccountId == UserID.Id);

                if (cart != null)
                {
                    o = new Order()
                    {
                        CustomerId = UserID.Id,
                        CartId = cart.Id,
                        TotalPaid = cart.GetTotalPrice(),
                        OrderStatus = OrderStatus.PaymentReceived,
                        CreatedDate = DateTime.Now
                    };

                    Context.orders.Add(o);
                    Context.SaveChanges();
                }
            }
           // return o;
        }

        public List<Order> GetAllOrders(string Token)
        {
            ApplicationUser? UserID = Context.Users.FirstOrDefault(a => a.token == Token);
            if (UserID != null)
            {
                return Context.orders.Include(c=> c.Cart).Include(u=>u.Customer).ToList();
            }
            return null;
        }

        public List<Order> getOrderForUser(string Token)
        {
            ApplicationUser? UserID = Context.Users.FirstOrDefault(a => a.token == Token);

            if (UserID != null)
            {
                var UserOrder = Context.orders.Where(x=> x.CustomerId == UserID.Id).ToList();
                return UserOrder;
            }

            return null;
        }

        public void UpdateUser()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser user(string Token)
        {
            ApplicationUser? User = Context.Users.FirstOrDefault(a => a.token == Token);

            return User;

        }
    }
}
