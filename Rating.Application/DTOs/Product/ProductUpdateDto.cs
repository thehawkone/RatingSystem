namespace Rating.Application.DTOs.Product;

public class ProductUpdateDto : ProductCreateDto
{
    public Guid ProductId { get; set; }
}