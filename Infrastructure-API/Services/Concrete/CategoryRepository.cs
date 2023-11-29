using ApplicationCore_API.Entities.Concrete;
using Infrastructure_API.Context;
using Infrastructure_API.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_API.Services.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AnyCategory(int id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }

        public bool AnyCategory(string categoryName)
        {
            return _context.Categories.Any(x => x.Name == categoryName);

        }

        public bool CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id && x.Status != ApplicationCore_API.Entities.Abstract.Status.Passive);
            category.DeletedDate = DateTime.Now;
            category.Status = ApplicationCore_API.Entities.Abstract.Status.Passive;
            _context.Categories.Update(category);
            return Save();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.Where(x => x.Status != ApplicationCore_API.Entities.Abstract.Status.Passive).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id && x.Status != ApplicationCore_API.Entities.Abstract.Status.Passive);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            category.UpdatedDate = DateTime.Now;
            category.Status = ApplicationCore_API.Entities.Abstract.Status.Modified;
            _context.Categories.Update(category);
            return Save();
        }
    }
}
