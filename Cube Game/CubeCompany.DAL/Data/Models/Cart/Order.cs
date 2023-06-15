using CubeGame.Data.Models.Account;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models.Cart
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived,

        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string CustomerId { get; set; }

        [ForeignKey("Cart")]
        [Required]
        public int CartId { get; set; }

        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus? OrderStatus { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public  double TotalPaid { get; set; }

        public virtual ApplicationUser? Customer { get; set; }

        public virtual Cart? Cart { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

}
