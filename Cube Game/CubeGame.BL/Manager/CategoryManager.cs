using CubeGame.BL.DTO;
using CubeGame.DAL.Data.Models;
using CubeGame.DAL.Repo.category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CubeGame.BL.Manager
{
    public class CategoryManager : ICategoryManager
    {
        ICategoryRepo IR { get; }
        public CategoryManager(ICategoryRepo _IR)
        {
            IR = _IR;
        }
        public void Add(CategoryDTO cd)
        {
            Category c = new Category();
            c.CategoryName = cd.CategoryName;
            IR.Add(c);
        }

        public void Delete(int id)
        {
            IR.Delete(id);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var ins = IR.GetAll();
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            foreach (var i in ins)
            {
                CategoryDTO dTO = new CategoryDTO()
                {
                    ID=i.ID,
                    CategoryName = i.CategoryName
                };
                categoryDTOs.Add(dTO);
            }
            return categoryDTOs;
        }


        public CategoryDTO GetById(int id)
        {
            var o = IR.GetById(id);
            CategoryDTO c = new CategoryDTO();
            c.CategoryName = o.CategoryName;
            c.ID = o.ID;
            return c;
        }

        public void Update(int id, CategoryDTO c)
        {
            var o = IR.GetById(id);
            o.CategoryName = c.CategoryName;
            IR.Update(id , o);
        }

    }
}
