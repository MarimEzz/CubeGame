using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Repo.category;
using CubeGame.Data.Context;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.product
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDBContext _context;
        ICategoryRepo IC;
        public ProductRepo(ApplicationDBContext context, ICategoryRepo iC)
        {
            _context = context;
            IC = iC;
        }
        public void AddProduct(Product p)
        {
            _context.Add(p);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var p = getProductByID(id);
            _context.Products.Remove(p);
            _context.SaveChanges();
        }

        public void EditProduct(int id, Product pD)
        {
            var P = _context.Products.FirstOrDefault(p => p.ProductId == id);
            P.ProductName = pD.ProductName;
            P.Description = pD.Description;
            P.Price = pD.Price;
            P.Discount = pD.Discount;
            P.CategoryId = pD.CategoryId;
            P.DeveloperName = pD.DeveloperName;
            P.RAM = pD.RAM;
            P.Processor = pD.Processor;
            P.ReleaseDate = pD.ReleaseDate;
            //P.platform = pD.platform;

            P.platform = (DAL.Data.Models.OS)pD.platform;
            P.IsComingSoon = pD.IsComingSoon;
            P.IsFreeGame = pD.IsFreeGame;
            P.IsGameOnSale = pD.IsGameOnSale;
            P.IsMostPlayed = pD.IsMostPlayed;
            P.IsNewRelease = pD.IsNewRelease;
            P.IsMostPopular = pD.IsMostPopular;
            P.IsRecentlyUpdated = pD.IsRecentlyUpdated;
            P.IsTopRated = pD.IsTopRated;
            P.IsTopSeller = pD.IsTopSeller;
            P.Rate = pD.Rate;
            _context.Products.Update(P);
            _context.SaveChanges();
        }
        public List<Image> GetImages(int Productid)
        {
            return _context.Images.Where(x => x.ProductId == Productid).ToList();
        }

        public void AddImage(Image I)
        {
            _context.Images.Add(I);
            _context.SaveChanges();
        }
        public List<Product> GetAll()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).ToList();
        }
        public List<Product> GetAllWithoutImage()
        {
            return _context.Products.ToList();
        }
        public List<Product> GetAllMostPopular()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.IsMostPopular == true).ToList();
        }
        public List<Product> GetAllMostPlayed()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.IsMostPlayed == true).ToList();
        }
        public List<Product> GetAllRecentlyUpdated()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.IsRecentlyUpdated == true).ToList();
        }
        public List<Product> GetAllNewRelease()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.IsNewRelease == true).ToList();
        }
        public List<Product> GetAllTopRated()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.Rate == 5).ToList();
        }
        public List<Product> GetAllTopSeller()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.IsTopSeller == true).ToList();
        }
        public List<Product> GetAllFreeGames()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.Price == 0).ToList();
        }
        public List<Product> GetAllGameOnSale()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.IsGameOnSale == true).ToList();
        }
        public List<Product> GetAllComingSoon()
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).Where(x => x.IsComingSoon == true).ToList();
        }

        public Product getProductByID(int id)
        {
            return _context.Products.Include(i => i.Images).Include(c => c.category).SingleOrDefault(g => g.ProductId == id);
        }

        public List<Product> GetProductsByCategory(int categoryid)
        {
            return _context.Products.Include(i => i.Images).Where(c=>c.CategoryId == categoryid).ToList();

        }

        public List<Product> GetProductsByPrice(int price)
        {
            return _context.Products.Include(i => i.Images).Where(p=>p.Price<=price).ToList();  
        }

        public List<Product> GetProductsByPlatform(OS platform)
        {
            return _context.Products.Include(i => i.Images).Where(p => p.platform==platform).ToList();
        }

        public List<Product> GetProductsByDeveloperName(string DeveloperName)
        {
            return _context.Products.Include(i => i.Images).Where(p => p.DeveloperName == DeveloperName).ToList();
        }
    }
}
