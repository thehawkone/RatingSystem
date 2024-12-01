using Microsoft.AspNetCore.Mvc;
using Rating.Application.DTOs.Product;
using Rating.Application.DTOs.Review;
using Rating.Application.Services;

namespace Rating.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _reviewService;

    public ReviewController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }
    
    [HttpPost("leave-review")]
    public async Task<IActionResult> LeaveReview(Guid userId, Guid productId, [FromBody] ReviewDto reviewDto)
    {
        await _reviewService.AddReviewAsync(userId, productId, reviewDto);
        return Ok("Отзыв добавлен");
    }

    [HttpPut("update-review")]
    public async Task<IActionResult> UpdateReview(Guid reviewId, [FromBody] ReviewDto reviewDto)
    {
        await _reviewService.UpdateReviewAsync(reviewId, reviewDto);
        return Ok("Отзыв успешно обновлён");
    }
    
    [HttpDelete("delete-review")]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        await _reviewService.DeleteReviewAsync(reviewId);
        return Ok("Отзыв успешно удалён");
    }
}