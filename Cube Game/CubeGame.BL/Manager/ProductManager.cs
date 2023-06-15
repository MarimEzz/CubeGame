
using CubeGame.BL.DTO;
using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Repo.category;
using CubeGame.DAL.Repo.product;
using CubeGame.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OS = CubeGame.BL.DTO.OS;

namespace CubeGame.BL.Manager
{
    public class ProductManager : IProductManager
    {
        IProductRepo IR { get; }
        ICategoryRepo IC { get; }

        public ProductManager(IProductRepo _IR, ICategoryRepo _IC)
        {
            IR = _IR;
            IC = _IC;
        }


        public List<Image> GetProductImages(int productId)
        {
            return IR.GetImages(productId);
        }

        public void AddProductImages(int productId, Image I)
        {
            I.ProductId = productId;
            I.Title = "image";
            IR.AddImage(I);
        }

        public void AddProduct(ProductDTO pD)
        {
            Product P = new Product();
            P.ProductName = pD.ProductName;
            P.Description = pD.Description;
            P.Price = pD.Price;
            P.Discount = pD.Discount;
            P.CategoryId = pD.CategoryId;
            P.DeveloperName = pD.DeveloperName;
            P.RAM = pD.RAM;
            P.Processor = pD.Processor;
            P.ReleaseDate = pD.ReleaseDate;

            OS enumValue = (OS)Enum.Parse(typeof(OS), pD.platform);

            P.platform = (DAL.Data.Models.OS)enumValue;
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
            IR.AddProduct(P);
        }

        public void DeleteProduct(int id)
        {
            IR.DeleteProduct(id);
        }

        public void EditProduct(int id, ProductDTO dTO)
        {
            var P = IR.getProductByID(id);

            P.ProductName = dTO.ProductName;
            P.Description = dTO.Description;
            P.Price = dTO.Price;
            P.Discount = dTO.Discount;
            P.CategoryId = dTO.CategoryId;
            P.DeveloperName = dTO.DeveloperName;
            P.RAM = dTO.RAM;
            P.Processor = dTO.Processor;
            P.ReleaseDate = dTO.ReleaseDate;
            // P.platform = dTO.platform;

            OS enumValue = (OS)Enum.Parse(typeof(OS), dTO.platform);

            P.platform = (DAL.Data.Models.OS)enumValue;

            P.IsComingSoon = dTO.IsComingSoon;
            P.IsFreeGame = dTO.IsFreeGame;
            P.IsGameOnSale = dTO.IsGameOnSale;
            P.IsMostPlayed = dTO.IsMostPlayed;
            P.IsNewRelease = dTO.IsNewRelease;
            P.IsMostPopular = dTO.IsMostPopular;
            P.IsRecentlyUpdated = dTO.IsRecentlyUpdated;
            P.IsTopRated = dTO.IsTopRated;
            P.IsTopSeller = dTO.IsTopSeller;
            P.Rate = dTO.Rate;

            IR.EditProduct(id, P);
        }
        public List<ProductDTO> GetAllWithoutImage()
        {
            var ins = IR.GetAllWithoutImage();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;
        }
        public List<ProductDTO> GetAll()
        {

            var ins = IR.GetAll();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName,
                    IsComingSoon = i.IsComingSoon,
                    IsFreeGame = i.IsFreeGame,
                    IsGameOnSale = i.IsGameOnSale,
                    IsMostPlayed = i.IsMostPlayed,
                    IsNewRelease = i.IsNewRelease,
                    IsMostPopular = i.IsMostPopular,
                    IsRecentlyUpdated = i.IsRecentlyUpdated,
                    IsTopRated = i.IsTopRated,
                    IsTopSeller = i.IsTopSeller,
                    Rate = i.Rate
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }

        public List<ProductDTO> GetAllComingSoon()
        {

            var ins = IR.GetAllComingSoon();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllGameOnSale()
        {

            var ins = IR.GetAllGameOnSale();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllMostPopular()
        {

            var ins = IR.GetAllMostPopular();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllMostPlayed()
        {

            var ins = IR.GetAllMostPlayed();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllRecentlyUpdated()
        {

            var ins = IR.GetAllRecentlyUpdated();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllNewRelease()
        {

            var ins = IR.GetAllNewRelease();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllTopRated()
        {

            var ins = IR.GetAllTopRated();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllTopSeller()
        {

            var ins = IR.GetAllTopSeller();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public List<ProductDTO> GetAllFreeGames()
        {

            var ins = IR.GetAllFreeGames();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;

        }
        public ProductDTO getProductByID(int id)
        {
            var i = IR.getProductByID(id);
            ProductDTO insDTo = new ProductDTO();
            if (i != null)
            {
                insDTo.ProductId = i.ProductId;
                insDTo.ProductName = i.ProductName;
                insDTo.Description = i.Description;
                insDTo.Price = i.Price;
                insDTo.Discount = i.Discount;
                insDTo.CategoryId = i.CategoryId;
                insDTo.DeveloperName = i.DeveloperName;
                insDTo.RAM = i.RAM;
                insDTo.Processor = i.Processor;
                insDTo.ReleaseDate = i.ReleaseDate;
                insDTo.platform = i.platform.ToString();
                insDTo.Picture = i.GetMainImage().ImageURL;
                insDTo.CategoryName = IC.GetById(i.CategoryId).CategoryName;
            }

            return insDTo;
        }

        public List<ProductDTO> GetProductsByCategory(int id)
        {
            var ins = IR.GetProductsByCategory(id);

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);
            }

            return productDTOs;
        }
        public List<ProductDTO> GetProductsByPrice(int price)
        {
            var ins = IR.GetProductsByPrice(price);

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);

            }

            return productDTOs;
        }

        public List<ProductDTO> GetProductsByPlatform(DAL.Data.Models.OS platform)
        {
            var ins = IR.GetProductsByPlatform(platform);

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);

            }

            return productDTOs;

        }

        public List<ProductDTO> GetProductsByDeveloperName(string DeveloperName)
        {
            var ins = IR.GetProductsByDeveloperName(DeveloperName);

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (var i in ins)
            {
                ProductDTO dTO = new ProductDTO()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Price = i.Price,
                    Discount = i.Discount,
                    CategoryId = i.CategoryId,
                    DeveloperName = i.DeveloperName,
                    RAM = i.RAM,
                    Processor = i.Processor,
                    ReleaseDate = i.ReleaseDate,
                    platform = i.platform.ToString(),
                    Picture = i.GetMainImage().ImageURL,
                    CategoryName = IC.GetById(i.CategoryId).CategoryName
                };
                productDTOs.Add(dTO);

            }

            return productDTOs;
        }
    }
}
