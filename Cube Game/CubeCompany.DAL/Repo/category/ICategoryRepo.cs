using CubeGame.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.category
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category c);
        void Update(int id, Category c);
        void Delete(int id);
    }
}
