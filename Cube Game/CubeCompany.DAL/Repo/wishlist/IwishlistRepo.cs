using CubeGame.DAL.Data.Models.Cart;
using CubeGame.DAL.Data.Models.wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.wishlist
{
    public interface IwishlistRepo
    {
        public Wishlist GetwishlisttItems(string token);
        public wishlistItam AddToWishlist(int id, string token);
        public void ClearWishList(string Token);
        public void RemoveFromWishList(int id, string token);
    }
}
