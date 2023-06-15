using CubeGame.DAL.Data.Models.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CartId { get; set; }
        public string OrderStatus { get; set; }
        public double TotalPaid { get; set; }
        public DateTime? CreatedDate { get; set; }
   
    }
}
