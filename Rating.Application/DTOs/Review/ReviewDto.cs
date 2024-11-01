namespace Rating.Application.DTOs.Review;

public class ReviewDto
{
    public Guid ReviewId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}