using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Discount { get; set; }

        public double Price { get; set; }

        public double PriceAfterDiscount { get; set; }

        public string Image { get; set; }
    }
}
