using CubeGame.BL.DTO;
using CubeGame.DAL.Repo.cart;
using CubeGame.DAL.Repo.category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.Manager
{
    public class CartManager : ICartManager
    {
        ICartRepo IR { get; }
        public CartManager(ICartRepo _IR)
        {
            IR = _IR;
        }
        public IEnumerable<CartDTO> GetAll(string token)
        {
            var ins = IR.GetCartItems(token);

            List<CartDTO> cartDTOs = new List<CartDTO>();

            foreach (var i in ins.CartItems)
            {
                CartDTO dTO = new CartDTO()
                {
                    Id = i.Product.ProductId,
                    ProductName = i.Product.ProductName,
                    Price = i.Price,
                    Discount = i.Discount,
                    PriceAfterDiscount = i.PriceAfterDiscount,
                    Image = i.Product.GetMainImage().ImageURL,
                };
                cartDTOs.Add(dTO);
            }

            return cartDTOs;
        }
    }
}
