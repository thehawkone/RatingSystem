using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Rating.Application.DTOs.Product;
using Rating.DataAccess;
using Rating.Domain.Abstractions;
using Rating.Domain.Models;

namespace Rating.Application.Services;

public class ProductService
{
    private readonly RatingDbContext _ratingDbContext;
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, RatingDbContext ratingDbContext)
    {
        _productRepository = productRepository;
        _ratingDbContext = ratingDbContext;
    }

    public async Task<bool> CreateProductAsync(ProductCreateDto productCreateDto)
    {
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            Name = productCreateDto.Name,
            Description = productCreateDto.Description,
            Category = productCreateDto.Category,
            AverageRating = 0.0
        };
        
        await _productRepository.CreateProductAsync(product);
        await _ratingDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateProductAsync(ProductUpdateDto productUpdateDto)
    {
        var product = await _productRepository.GetProductByIdAsync(productUpdateDto.ProductId);
        if (product == null) {
            throw new Exception("Неверный идентификатор продукта");
        }
        
        product.Name = productUpdateDto.Name;
        product.Description = productUpdateDto.Description;
        product.Category = productUpdateDto.Category;
        
        await _productRepository.UpdateProductAsync(product);
        await _ratingDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null) {
            throw new Exception("Неверный идентификатор продукта");
        }

        await _productRepository.DeleteProductAsync(productId);
        await _ratingDbContext.SaveChangesAsync();

        return true;
    }
}