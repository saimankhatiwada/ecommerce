using Ecommerce.Application.Abstractions.Messaging;

namespace Ecommerce.Application.Users.LogInUser;


public sealed record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
