namespace Ecommerce.Domain.Users;
public sealed class Role
{
    public static readonly Role None = new(0, string.Empty);
    public static readonly Role Registered = new(1, "Registered");
    public static readonly Role Management = new(2, "Management");

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }
    public ICollection<User> Users { get; init; } = new List<User>();
    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
    public static Role FormRole(string name) => All.FirstOrDefault(r => r.Name == name) ?? 
        throw new ApplicationException("The role is invalid.");
    public static Role CheckRole(string name) => All.FirstOrDefault(r => r.Name == name) ?? None;
    public static readonly IReadOnlyCollection<Role> All = new[]
    {
        Registered,
        Management
    };
}
