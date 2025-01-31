using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    //
    public class CategoryRepository : ICategoryRepository
    {
        ApiDbToCodeContext _apiDbToCodeContext;
        public CategoryRepository(ApiDbToCodeContext ApiDbToCodeContext)
        {
            _apiDbToCodeContext = ApiDbToCodeContext;
           

        }



        public async Task<List<Category>> GetAllCategory()
        {
           return await _apiDbToCodeContext.Categories.ToListAsync();

        }
    }
}
