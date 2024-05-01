using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Users.Events;

namespace Ecommerce.Domain.Users;

public sealed class User : Entity<UserId>
{
    private readonly List<Role> _roles = new();

    private User(UserId id, FirstName firstName, LastName lastName, Email email, ImageName imageName)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ImageName = imageName;
    }

    private User()
    {
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public ImageName ImageName { get; private set; }

    public string IdentityId { get; private set; } = string.Empty;

    public IReadOnlyCollection<Role> Roles => _roles.ToList();

    public static User Create(FirstName firstName, LastName lastName, Email email, ImageName imageName, Role role)
    {
        var user = new User(UserId.New(), firstName, lastName, email, imageName);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        user._roles.Add(role);

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
