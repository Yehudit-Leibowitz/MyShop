using Entity;

namespace Repository
{
    public interface IRatingRepository
    {
        Task AddRating(Rating rating);
    }
}