using CubeGame.BL.DTO;
using CubeGame.DAL.Repo.cart;
using CubeGame.DAL.Repo.wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.Manager
{
    public class WishlistManager : IWishlistManager
    {
        private  IwishlistRepo Iwish{ get; }
        public WishlistManager(IwishlistRepo _Iw)
        {
           Iwish = _Iw;
        }

        public IEnumerable<wishlistDto> GetAll(string token)
        {
            var wishlistItems = Iwish.GetwishlisttItems(token);

            List<wishlistDto> wishlistDtos = new List<wishlistDto>();

            foreach (var wish in wishlistItems.WishlistItams)
            {
                var wishlistDto = new wishlistDto()
                {
                    Id = wish.productId,
                    ProductName = wish.Product.ProductName,
                    Discount = wish.Product.Discount,
                    Price = wish.Product.Price,
                    PriceAfterDiscount = wish.Product.PriceAfterDiscount(),
                    Image = wish.Product.GetMainImage().ImageURL
                };

                wishlistDtos.Add(wishlistDto);
            }


            return wishlistDtos;
        }

    }
}

        //public IEnumerable<wishlistDto> GetAll(string token)
        //{
        //   var wish=Iwish.GetwishlisttItems(token);

        //    var wishDto = new List<wishlistDto>();
        //    wishlistDto wishlist = new wishlistDto()
        //    {
        //        Id = wish.ProductID,
        //        ProductName = wish.Product.ProductName,
        //        Discount = wish.Product.Discount,
        //        Price = wish.Product.Price,
        //        PriceAfterDiscount = wish.Product.PriceAfterDiscount(),
        //        Image = wish.Product.GetMainImage().ImageURL
        //    };
        //   wishDto.Add(wishlist);   
        //    return wishDto;}