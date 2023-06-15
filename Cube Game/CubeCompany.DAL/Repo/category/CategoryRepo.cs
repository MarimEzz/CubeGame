using CubeGame.DAL.Data.Models;
using CubeGame.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGame.DAL.Repo.category
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public void Add(Category c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var c = _context.Categories.FirstOrDefault(g => g.ID == id);
            _context.Remove(c);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {

            return _context.Categories.Include(x=>x.Products).ToList();
        }
        

        public Category GetById(int id)
        {
            return _context.Categories.Include(p=>p.Products).SingleOrDefault(g => g.ID == id);
        }

        public void Update(int id, Category c)
        {
            var old = _context.Categories.FirstOrDefault(g => g.ID == id);
            old.CategoryName = c.CategoryName;
            _context.Update(old);
            _context.SaveChanges();
        }


       
    }
}
