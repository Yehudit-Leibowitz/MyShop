using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        public async Task<List<Category>> GetAllCategory()
        {
            return await categoryRepository.GetAllCategory();
        }

    }
}

