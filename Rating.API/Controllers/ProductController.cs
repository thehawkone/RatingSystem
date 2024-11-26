using Microsoft.AspNetCore.Mvc;
using Rating.Application.DTOs.Product;
using Rating.Application.Services;

namespace Rating.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
    {
        await _productService.CreateProductAsync(productCreateDto);
        return Ok("Продукт успешно создан");
    }

    [HttpPut("update-product")]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto)
    {
        await _productService.UpdateProductAsync(productUpdateDto);
        return Ok("Продукт успешно обновлён");
    }

    [HttpDelete("delete-product")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _productService.DeleteProductAsync(productId);
        return Ok("Продукт успешно удалён");
    }
}