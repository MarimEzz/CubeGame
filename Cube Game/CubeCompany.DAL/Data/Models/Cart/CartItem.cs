using CubeGame.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models.Cart
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [DataType(DataType.Currency)]
        public double PriceAfterDiscount { get; set; }

        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }

        [DefaultValue(0.00)]
        [Range(0.00, 100.00)]
        public double Discount { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }

    }
}