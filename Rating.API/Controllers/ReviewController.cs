using Microsoft.AspNetCore.Mvc;
using Rating.Application.DTOs.Review;
using Rating.Application.Services;

namespace Rating.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _reviewService;
    private readonly UserService _userService;

    public ReviewController(ReviewService reviewService, UserService userService)
    {
        _reviewService = reviewService;
        _userService = userService;
    }
    
    [HttpPost("{productId}")]
    public async Task<IActionResult> LeaveReview(Guid userId, [FromBody] ReviewDto reviewDto)
    {
        var review = await _reviewService.AddReviewAsync(userId, reviewDto);
        return Ok("Отзыв добавлен");
    }
}