using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {

        ApiDbToCodeContext _apiDbToCodeContext;
        public RatingRepository(ApiDbToCodeContext ApiDbToCodeContext)
        {
            _apiDbToCodeContext = ApiDbToCodeContext;

        }


        public async Task AddRating(Rating rating)
        {
            await _apiDbToCodeContext.Ratings.AddAsync(rating);
            await _apiDbToCodeContext.SaveChangesAsync();


        }
    }
}
