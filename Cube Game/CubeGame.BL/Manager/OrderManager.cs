using CubeGame.BL.DTO;
using CubeGame.DAL.Repo.cart;
using CubeGame.DAL.Repo.order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.Manager
{
    public class OrderManager : IOrderManager
    {
        IOrderRepo IR { get; }
        public OrderManager(IOrderRepo _IR)
        {
            IR = _IR;
        }

        public IEnumerable<OrderDTO> GetAllOrderFouUser(string token)
        {
            var orders = IR.getOrderForUser(token);

            List<OrderDTO> DTOs = new List<OrderDTO>();

            foreach (var i in orders)
            {
                OrderDTO dTO = new OrderDTO()
                {
                    Id = i.Id,
                    CustomerId = i.CustomerId,
                    CustomerName = i.Customer.UserName,
                    CartId = i.CartId,
                    OrderStatus = i.OrderStatus.ToString(),
                    TotalPaid = i.TotalPaid,
                    CreatedDate = i.CreatedDate
                };
                DTOs.Add(dTO);
            }

            return DTOs;
        }

        public IEnumerable<OrderDTO> GetAllOrders(string token)
        {
            var orders = IR.GetAllOrders(token);

            List<OrderDTO> DTOs = new List<OrderDTO>();

            foreach (var i in orders)
            {
                OrderDTO dTO = new OrderDTO()
                {
                    Id = i.Id,
                    CustomerId = i.CustomerId,
                    CustomerName = i.Customer.FirstName,
                    CartId = i.CartId,
                    OrderStatus = i.OrderStatus.ToString(),
                    TotalPaid = i.TotalPaid,
                    CreatedDate = i.CreatedDate
                };
                DTOs.Add(dTO);
            }
            return DTOs;
        }
    }
}
