using CubeGame.DAL.Data.Models.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models.wishlist
{
    public class wishlistItam
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [DataType(DataType.Currency)]
        public double PriceAfterDiscount { get; set; }

        [DefaultValue(0.00)]
        [Range(0.00, 100.00)]
        public double Discount { get; set; }
        [ForeignKey(" Wishlist")]
        [Required]
        public int wishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }

        [ForeignKey("Product")]
        [Required]
        public required int productId { get; set; }
        public virtual Product? Product { get; set; }

    }
}
