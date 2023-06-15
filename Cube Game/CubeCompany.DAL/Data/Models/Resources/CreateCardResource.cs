using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models.Resources
{
    public record CreateCardResource
    (string Name,
    string Number,
    string ExpiryYear,
    string ExpiryMonth,
    string Cvc);
}
