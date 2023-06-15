using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Data.Models.wishlist;
using CubeGame.Data.Context;
using CubeGame.Data.Helper;
using CubeGame.Data.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.wishlist
{
    public class WishListRepo : IwishlistRepo
    {
        private readonly ApplicationDBContext context;

        public WishListRepo(ApplicationDBContext _context)
        {
            context = _context;
        }
        public Wishlist GetwishlisttItems(string Token)
        {

            ApplicationUser? account = context.Users.FirstOrDefault(a => a.token == Token);

            Wishlist? wishlistItems = context.wishlists.Include(w => w.WishlistItams)
                .ThenInclude(w => w.Product)
                .ThenInclude(w => w.Images)
                .FirstOrDefault(w => w.AccountID == account.Id);

            return wishlistItems;
        }

        public wishlistItam AddToWishlist(int id, string token)
        {


            Product? product = context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                ApplicationUser? account = context.Users.FirstOrDefault(a => a.token == token);

                if (account != null)
                {
                    Wishlist? existingWishlist = context.wishlists.Include(w => w.WishlistItams).FirstOrDefault(w => w.AccountID == account.Id);

                    if (existingWishlist != null)
                    {
                        wishlistItam? wishlistItams = existingWishlist.WishlistItams.FirstOrDefault(w => w.productId == id);
                        if (wishlistItams != null)
                        {
                            return wishlistItams;
                        }
                        else
                        {

                            // Wishlist does not exist, create a new wishlist item
                            wishlistItam? newWishlist = new wishlistItam
                            {
                                productId = product.ProductId,
                                Price = product.Price,
                                PriceAfterDiscount = product.PriceAfterDiscount(),
                                Discount = product.Discount,
                                wishlistId = existingWishlist.Id
                            };

                            context.WishlistItams.Add(newWishlist);
                            context.SaveChanges();
                            return newWishlist;
                        }


                    }
                    else
                    {
                        existingWishlist = new()
                        {
                            AccountID = account.Id,
                            IsActive = true
                        };
                        context.Add(existingWishlist);
                        context.SaveChanges();
                        wishlistItam? newWishlist = new wishlistItam
                        {
                            productId = product.ProductId,
                            Price = product.Price,
                            PriceAfterDiscount = product.PriceAfterDiscount(),
                            Discount = product.Discount,
                            wishlistId = existingWishlist.Id
                        };


                        context.WishlistItams.Add(newWishlist);
                        context.SaveChanges();

                        return newWishlist;
                    }


                }
            }
            return null;

        }
        public void ClearWishList(string Token)
        {
            ApplicationUser? account = context.Users.FirstOrDefault(a => a.token == Token);

            var wishlist = context.wishlists.Include(w => w.WishlistItams).FirstOrDefault(w => w.AccountID == account.Id);

            var wishlistItems = wishlist.WishlistItams.Where(w => w.wishlistId == wishlist.Id).ToList();

            foreach (var wish in wishlistItems)
            {
                context.Remove(wish);
                context.SaveChanges();
            }


        }
        public void RemoveFromWishList(int id, string token)
        {


            Product? product = context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product != null)
            {
                ApplicationUser? account = context.Users.FirstOrDefault(a => a.token == token);

                if (account != null)
                {
                    Wishlist? existingWishlist = context.wishlists.Include(w => w.WishlistItams).FirstOrDefault(w => w.AccountID == account.Id);
                    if (existingWishlist != null)
                    {
                        wishlistItam? wishlistItams = existingWishlist.WishlistItams.FirstOrDefault(w => w.productId == id);
                        if (wishlistItams != null)
                        {

                            context.Remove(wishlistItams);
                            context.SaveChanges();

                        }




                    }



                }
            }
        }
    }
}

   