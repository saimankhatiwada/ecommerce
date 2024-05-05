namespace Ecommerce.Domain.Users;

public sealed class Permission
{
    public static readonly Permission UsersReadOwn = new(1, "users:readown");

    public static readonly Permission UsersRead = new(2, "users:read");

    public static readonly Permission UsersUpdate = new(3, "users:update");

    public static readonly Permission UsersDelete = new(4, "users:delete");

    public static readonly Permission CategoriesCreate = new(5, "categories:create");

    public static readonly Permission CategoriesUpdate = new(6, "categories:update");

    public static readonly Permission CategoriesDelete = new(7, "categories:delete");

    public static readonly Permission SubCategoriesCreate = new(8, "subcategories:create");

    public static readonly Permission SubCategoriesUpdate = new(9, "subcategories:update");

    public static readonly Permission SubCategoriesDelete = new(10, "subcategories:delete");

    public static readonly Permission ProductsCreate = new(11, "products:create");

    public static readonly Permission ProductsUpdate = new(12, "products:update");

    public static readonly Permission ProductsDelete = new(13, "products:delete");

    private Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }
}
