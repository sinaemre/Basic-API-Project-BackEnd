using ApplicationCore_API.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_API.Services.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategory(int id);

        bool AnyCategory(int id);
        bool AnyCategory(string categoryName);

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int id);

        bool Save();



    }
}
