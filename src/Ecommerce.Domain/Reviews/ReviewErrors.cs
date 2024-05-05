using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly Error NotFound = new(
        "Review.NotFound",
        "The review with the specified identifier was not found");
}
