using Rating.Domain.Models;

namespace Rating.Domain.Abstractions;

public interface IReviewRepository
{
    Task<Review> GetReviewByIdAsync(Guid reviewId);
    Task CreateReviewAsync(Review review);
    Task UpdateReviewAsync(Review review);
    Task DeleteReviewAsync(Guid reviewId);
}