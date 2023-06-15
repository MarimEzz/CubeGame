using CubeGame.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.product
{
    public interface IProductRepo
    {
        public List<Product> GetAll();
        public List<Product> GetProductsByCategory(int id);
        public List<Product> GetProductsByPrice(int price);
        public List<Product> GetProductsByPlatform(OS platform);
        public List<Product> GetProductsByDeveloperName(string DeveloperName);

        public List<Product> GetAllWithoutImage();
        public List<Product> GetAllComingSoon();
        public List<Product> GetAllGameOnSale();
        public List<Product> GetAllFreeGames();
        public List<Product> GetAllTopSeller();
        public List<Product> GetAllTopRated();
        public List<Product> GetAllNewRelease();
        public List<Product> GetAllRecentlyUpdated();
        public List<Product> GetAllMostPlayed();
        public List<Product> GetAllMostPopular();

        public Product getProductByID(int id);
        public void AddProduct(Product p);

        public void EditProduct(int id, Product P);

        public void DeleteProduct(int id);

        public List<Image> GetImages(int id);

        public void AddImage(Image I);

    }
}
