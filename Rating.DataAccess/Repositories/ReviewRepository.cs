using Rating.Domain.Abstractions;
using Rating.Domain.Models;

namespace Rating.DataAccess.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly RatingDbContext _context;

    public ReviewRepository(RatingDbContext context)
    {
        _context = context;
    }

    public async Task<Review> GetReviewByIdAsync(Guid reviewId)
    {
        return await _context.Reviews.FindAsync(reviewId);
    }

    public async Task CreateReviewAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(Guid reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review != null) {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}