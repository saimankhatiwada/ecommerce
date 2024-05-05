using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Categories.Shared;
using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Application.Categories.GetAllCategory;

internal sealed class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryQuery, IReadOnlyList<CategoryResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            id AS Id,
            name AS Name
        FROM categories
        """;

        IEnumerable<CategoryResponse> categoriesResponse = await connection.QueryAsync<CategoryResponse>(sql, cancellationToken);

        return categoriesResponse.ToList().AsReadOnly();
    }

}
