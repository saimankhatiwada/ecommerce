using Ecommerce.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });

        builder.HasData(
            new RolePermission
            {
                RoleId = Role.Registered.Id,
                PermissionId = Permission.UsersReadOwn.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.UsersReadOwn.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.UsersRead.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.UsersUpdate.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.UsersDelete.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.CategoriesCreate.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.CategoriesUpdate.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.CategoriesDelete.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.SubCategoriesCreate.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.SubCategoriesUpdate.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.SubCategoriesDelete.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.ProductsCreate.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.ProductsUpdate.Id
            },
            new RolePermission
            {
                RoleId = Role.Management.Id,
                PermissionId = Permission.ProductsDelete.Id
            });
    }
}
