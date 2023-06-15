using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models.Resources
{
    public record ChargeResource(
    string ChargeId,
    string CustomerId,
    string ReceiptEmail);
}
