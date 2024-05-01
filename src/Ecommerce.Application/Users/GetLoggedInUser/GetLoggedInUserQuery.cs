using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Users.shared;

namespace Ecommerce.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
