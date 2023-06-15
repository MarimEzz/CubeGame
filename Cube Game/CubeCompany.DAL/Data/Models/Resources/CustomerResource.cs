using CubeGame.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Data.Models.Resources
{
    public record CustomerResource(
    string CustomerId,
    string Email,
    string Name
    );
}
