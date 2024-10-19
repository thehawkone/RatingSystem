using Rating.Domain.Abstractions;
using Rating.Domain.Models;

namespace Rating.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly RatingDbContext _context;

    public ProductRepository(RatingDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null) {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}