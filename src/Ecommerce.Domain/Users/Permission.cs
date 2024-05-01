namespace Ecommerce.Domain.Users;

public sealed class Permission
{
    public static readonly Permission UsersReadOwn = new(1, "users:readown");
    public static readonly Permission UsersRead = new(2, "users:read");
    public static readonly Permission UsersUpdate = new(3, "users:update");
    public static readonly Permission UsersDelete = new(4, "users:delete");
    private Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }
}
