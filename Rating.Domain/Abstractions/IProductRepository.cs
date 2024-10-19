using Rating.Domain.Models;

namespace Rating.Domain.Abstractions;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(Guid productId);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid productId);
}