using CubeGame.BL.DTO;
using CubeGame.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.BL.Manager
{
    public interface ICategoryManager
    {
        IEnumerable<CategoryDTO> GetAll();
       // IEnumerable<CategoryDTO> GetAl(int categoryId);
        CategoryDTO GetById(int id);
        void Add(CategoryDTO c);
        void Update(int id, CategoryDTO c);
        void Delete(int id); 
       //List<Product> GetAllproduct(int categoryId);
    }
}
