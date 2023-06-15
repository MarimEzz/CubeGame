using CubeGame.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.DTO
{
    public enum OS
    {
        Windows, Linux, Mac
    }
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public  string ProductName { get; set; }
        public  string Description { get; set; }
        public  double Price { get; set; }
        public double Discount { get; set; }
        public double? Rate { get; set; }
        public int CategoryId { get; set; }           
        public string ? CategoryName { get; set; }
        public string DeveloperName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; }
        public string platform { get; set; }
        public bool IsMostPopular { get; set; }
        public bool IsMostPlayed { get; set; }
        public bool IsFreeGame { get; set; }
        public bool IsGameOnSale { get; set; }
        public bool IsTopSeller { get; set; }
        public bool IsTopRated { get; set; }
        public bool IsRecentlyUpdated { get; set; }
        public bool IsNewRelease { get; set; }
        public bool IsComingSoon { get; set; }
        public string? Picture { get; set; } 
    }
}
