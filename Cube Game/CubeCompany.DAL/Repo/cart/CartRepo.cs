using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Data.Models.Cart;
using CubeGame.Data.Context;
using CubeGame.Data.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.cart
{
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDBContext Context;
     
        public CartRepo(ApplicationDBContext _context)
        {
            Context = _context;          
        }

        public Product AddToCart(int Product_id , string Token)
        {
            Cart? cart = null;

            Product? product = Context.Products.FirstOrDefault(p => p.ProductId == Product_id);

            if (product != null)
            {
                ApplicationUser? account = Context.Users.FirstOrDefault(a => a.token == Token);

                if (account != null)
                {
                    cart = Context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.AccountId == account.Id);

                    if (cart != null)
                    {
                        CartItem? cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == Product_id);

                        if (cartItem != null)
                        {
                            cartItem.Quantity = 1;
                        }

                        else
                        {
                            cartItem = new CartItem()
                            {
                                ProductId = product.ProductId,
                                Quantity = 1,
                                Price = product.Price,
                                Discount = product.Discount,
                                PriceAfterDiscount = product.PriceAfterDiscount(),                               
                                CartId = cart.Id,
                            };

                            Context.CartItems.Add(cartItem);
                        }

                        cart.TotalPrice = cart.GetTotalPrice();
                        Context.SaveChanges();

                    }
                    else
                    {
                        cart = new()
                        {
                            AccountId = account.Id,
                            IsActive = true,
                            TotalPrice = 0,
                            //CreatedDate = DateTime.UtcNow,
                            CreatedDate = DateTime.Now
                        };
                        Context.Add(cart);
                        Context.SaveChanges();
                        CartItem cartItem = new CartItem()
                        {
                            ProductId = product.ProductId,
                            Quantity = 1,
                            Price = product.Price,
                            Discount = product.Discount,
                            PriceAfterDiscount = product.PriceAfterDiscount(),
                            CartId = cart.Id,
                        };

                        Context.CartItems.Add(cartItem);
                        Context.SaveChanges();
                        cart.TotalPrice = cart.GetTotalPrice();
                        Context.SaveChanges();
                        
                    }
                }

              
            }

            return product;
        }

        public void ClearCart(string Token)
        {
            ApplicationUser? account = Context.Users.FirstOrDefault(a => a.token == Token);

            Cart? c = Context.Carts.FirstOrDefault(x=> x.AccountId == account.Id);
      
            var cartItems = Context.CartItems.Where(
                i => i.CartId == c.Id).ToList();

            foreach (var cartItem in cartItems)
            {
                Context.Remove(cartItem);
                Context.SaveChanges();
            }
           
            c.TotalPrice = c.GetTotalPrice();

            Context.SaveChanges();
        }

        public Cart GetCartItems(string Token)
        {
            ApplicationUser? account = Context.Users.FirstOrDefault(a => a.token == Token);

            Cart? cart = Context.Carts.Include(c => c.CartItems).
                ThenInclude(ci => ci.Product).ThenInclude(p => p.Images).
                FirstOrDefault(C => C.AccountId == account.Id);
            
            return cart;
        }

        public Product RemoveFromCart(int productId , string Token)
        {
            Cart? cart = null;

            Product? product = Context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {

                ApplicationUser? account = Context.Users.FirstOrDefault(a => a.token == Token);

                if (account != null)
                {
                    cart = Context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.AccountId == account.Id);

                    if (cart != null)
                    {
                        CartItem? cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                        if (cartItem != null)
                        {                     
                            Context.Remove(cartItem);

                            Context.SaveChanges();
                        }

                        cart.TotalPrice = cart.GetTotalPrice();
                     
                        Context.SaveChanges();
                      
                    }
                }
               
            }

            return product;
        }
    }
}
