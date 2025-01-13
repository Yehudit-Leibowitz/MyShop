using Entity;

namespace DTO
{
    public record ProductDTO(int ProductId, string? ProductName , decimal? Price , string? Description  ,string? CategoryCategoryName, string? Image);
}
