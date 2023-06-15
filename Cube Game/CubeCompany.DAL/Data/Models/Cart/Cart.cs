using CubeGame.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models.Cart;

public class Cart
{

    [Key]
    public int Id { get; set; }

    [ForeignKey("ApplicationUser")]
    [Required]
    public string AccountId { get; set; }

    public double TotalPrice { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ApplicationUser? Account { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; } = true;

  
        public double GetTotalPrice()
        {
            double total = 0;
            if (CartItems.Count > 0)
            {
                foreach (var item in CartItems)
                    total += (item.Price - item.Price * (item.Discount / 100)) * item.Quantity;
            }
            return total;
        }

    
}

