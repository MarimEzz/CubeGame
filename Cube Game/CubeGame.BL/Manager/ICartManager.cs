using CubeGame.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.Manager
{
    public interface ICartManager
    {
        IEnumerable<CartDTO> GetAll(string token);
    }
}
