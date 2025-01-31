using Entity;

namespace service
{
    public interface IRatingService
    {
        Task AddRating(Rating rating);
    }
}