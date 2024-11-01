using Rating.Application.DTOs.Review;
using Rating.DataAccess;
using Rating.Domain.Abstractions;
using Rating.Domain.Models;

namespace Rating.Application.Services;

public class ReviewService
{
    private readonly RatingDbContext _ratingDbContext;
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository, RatingDbContext ratingDbContext)
    {
        _reviewRepository = reviewRepository;
        _ratingDbContext = ratingDbContext;
    }

    public async Task<Review> AddReviewAsync(Guid userId, ReviewDto reviewDto)
    {
        if (reviewDto.Rating < 0 || reviewDto.Rating > 5) {
            throw new ArgumentException("Оценка не может быть меньше 0 и больше 5");
        }

        var review = new Review
        {
            UserId = userId,
            ReviewId = reviewDto.ReviewId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment
        };
        
        await _reviewRepository.CreateReviewAsync(review);
        await _ratingDbContext.SaveChangesAsync();
        
        return review;
    }
}