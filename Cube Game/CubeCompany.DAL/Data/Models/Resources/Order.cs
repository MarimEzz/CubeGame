using CubeGame.Data.Models.Account;
using CubeGame.DAL.Data.Models.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace CubeGame.DAL.Data.Models.Resources
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
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public required int CustomerId { get; set; }

        [ForeignKey("Cart")]
        [Required]
        public required int CartId { get; set; }

        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus? OrderStatus { get; set; }

        [DataType(DataType.Currency)]
        public required double TotalPaid { get; set; }

        public string PaymentIntentId { get; set; }

        public virtual ApplicationUser? Customer { get; set; }

        public virtual Cart.Cart? Cart { get; set; }

    }
}
