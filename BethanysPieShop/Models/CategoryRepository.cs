using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext dbContext;

        public CategoryRepository(AppDbContext dbContext) 
            => this.dbContext = dbContext;

        public IEnumerable<Category> AllCategories 
            => dbContext.Categories;
    }
}
