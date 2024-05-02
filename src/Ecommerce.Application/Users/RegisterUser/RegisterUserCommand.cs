using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string MobileNumber,
    string Role,
    string ImageName,
    string ImageFileContentType,
    Stream ImageFileStream) : ICommand<Guid>;
