using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Data.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.cart
{
    public interface ICartRepo
    {    
        public Product AddToCart(int Product_id, string userId);
        public Cart GetCartItems(string user_id);

        public void ClearCart(string Token);
        public Product RemoveFromCart(int id , string token);

        //public double GetCartTotal();
    }
}
