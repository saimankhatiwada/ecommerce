using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Products;
using Ecommerce.Domain.Users;

namespace Ecommerce.Domain.Reviews;

public sealed class Review : Entity<ReviewId>
{
    private Review(
        ReviewId id, 
        ProductId productId, 
        UserId userId,
        Rating rating,
        Comment comment,
        CreatedOnUtc createdOnUtc)
        : base(id)
    {
        ProductId = productId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreatedOnUtc = createdOnUtc;
    }

    private Review()
    {
    }

    public ProductId ProductId { get; private set; }

    public UserId UserId { get; private set; }

    public Rating Rating { get; private set; }

    public Comment Comment { get; private set; }

    public CreatedOnUtc CreatedOnUtc { get; private set; }

    public static Review Create(
        ProductId productId, 
        UserId userId, 
        Rating rating, 
        Comment comment, 
        CreatedOnUtc createdOnUtc)
    {
        var review = new Review(
            ReviewId.New(), 
            productId, 
            userId, 
            rating, 
            comment, 
            createdOnUtc);

        return review;
    }
}
