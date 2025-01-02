using Microsoft.EntityFrameworkCore;
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

    public async Task AddReviewAsync(Guid userId, Guid productId, ReviewDto reviewDto)
    {
        if (reviewDto.Rating is < 1 or > 5) {
            throw new ArgumentException("Оценка не может быть меньше 0 и больше 5");
        }

        var review = new Review
        {
            UserId = userId,
            ReviewId = Guid.NewGuid(),
            ProductId = productId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment
        };
        
        await _reviewRepository.CreateReviewAsync(review);
        await UpdateProductRatingAsync(productId);
        await _ratingDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateReviewAsync(Guid reviewId, ReviewDto reviewDto)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
        
        if (review == null) {
            throw new Exception("Неверный идентификатор продукта");
        }
        
        review.Comment = reviewDto.Comment;
        review.Rating  = reviewDto.Rating;
        
        if (reviewDto.Rating is < 1 or > 5) {
            throw new ArgumentException("Оценка не может быть меньше 0 и больше 5");
        }
        
        await UpdateProductRatingAsync(review.ProductId);
        await _reviewRepository.UpdateReviewAsync(review);
        await _ratingDbContext.SaveChangesAsync();
    }
    
    public async Task DeleteReviewAsync(Guid reviewId)
    {
        var product = await _reviewRepository.GetReviewByIdAsync(reviewId);
        if (product == null) {
            throw new Exception("Неверный идентификатор продукта");
        }

        await _reviewRepository.DeleteReviewAsync(reviewId);
        await UpdateProductRatingAsync(product.ProductId);
        await _ratingDbContext.SaveChangesAsync();
    }

    private async Task UpdateProductRatingAsync(Guid productId)
    {
        var reviews = await _ratingDbContext.Reviews
            .Where(r => r.ProductId == productId)
            .ToListAsync();
        
        var averageRating = reviews.Any() 
            ? reviews.Average(r => r.Rating) 
            : 0;
        
        var product = await _ratingDbContext.Products
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product != null) {
            product.AverageRating = averageRating;
            _ratingDbContext.Products.Update(product);
        }
           
        await _ratingDbContext.SaveChangesAsync();
    }
}