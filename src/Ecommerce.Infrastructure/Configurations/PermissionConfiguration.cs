using Ecommerce.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(permission => permission.Id);

        builder.HasData(
            Permission.UsersReadOwn, 
            Permission.UsersRead,
            Permission.UsersUpdate,
            Permission.UsersDelete,
            Permission.CategoriesCreate,
            Permission.CategoriesUpdate,
            Permission.CategoriesDelete,
            Permission.SubCategoriesCreate,
            Permission.SubCategoriesUpdate,
            Permission.SubCategoriesDelete,
            Permission.ProductsCreate,
            Permission.ProductsUpdate,
            Permission.ProductsDelete);
    }
}
