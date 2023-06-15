using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.DTO
{
    public class ImageDTO
    {
        public virtual ICollection<IFormFile> Picture { get; set; } = new List<IFormFile>();
    }
}
