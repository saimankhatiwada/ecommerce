using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Categories.Shared;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;

namespace Ecommerce.Application.Categories.GetCategory;

internal sealed class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, CategoryResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            id AS Id,
            name AS Name
        FROM categories
        WHERE id = @Id
        """;

        CategoryResponse? categoryResponse = await connection.QueryFirstOrDefaultAsync<CategoryResponse>(sql, new
        {
            request.Id
        });

        if(categoryResponse is null)
        {
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);
        }

        return categoryResponse;
    }

}
