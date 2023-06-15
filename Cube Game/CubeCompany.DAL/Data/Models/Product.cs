using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models
{
    public enum OS
    {
        Windows , Linux , Mac
    }
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public  string ProductName { get; set; }

        [Required]
        public  string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public  double  Price { get; set; }


        [Range(0, 100)]
        [RegularExpression("^(([1-9]*)|(([1-9]*)\\.([0-9]*)))$")]
        [DefaultValue(0.00)]
        public double Discount { get; set; }


        [RegularExpression("^(([1-9*)|(([1-9]*)\\.([0-9]*)))$")]
        [DefaultValue(0.00)]
        public double ? Rate { get; set; }

        [Required]
        [ForeignKey("Category")]

        public  int CategoryId { get; set; }

        public Category category { get; set; }
        [Required]
        public string DeveloperName { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string Processor { get; set; }

        public int RAM { get; set; }

        [EnumDataType(typeof(OS))]
        public OS platform { get; set; }

        public bool IsMostPopular { get; set; }
        public bool IsMostPlayed { get; set; }
        public bool IsFreeGame { get; set; }
        public bool IsGameOnSale { get; set; }
        public bool IsTopSeller { get; set; }
        public bool IsTopRated { get; set; }
        public bool IsRecentlyUpdated { get; set; }
        public bool IsNewRelease { get; set; }
        public bool IsComingSoon { get; set; }

        public virtual ICollection<Image> ? Images { get; set; } = new List<Image>();

        public double PriceAfterDiscount() => Price * (1 - Discount / 100);
        public double DiscountedAmount() => Price * (Discount / 100);
        public Image? GetMainImage()
        {
            if (Images.Count == 0) return null;
            return Images.FirstOrDefault();
        }

   
    }
}
