namespace Rating.Application.DTOs.Product;

public class ProductReadDto : ProductUpdateDto
{
    public double AverageRating { get; set; }
}