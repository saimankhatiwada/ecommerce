namespace Ecommerce.Api.Users;

public sealed record RegisterUserRequest(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string Role,
    IFormFile ImageFile);
