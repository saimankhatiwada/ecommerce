using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Categories.Shared;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Categories;

namespace Ecommerce.Application.Categories.GetCategoryName;

internal sealed class GetCategoryNameQueryHandler : IQueryHandler<GetCategoryNameQuery, CategoryNameResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetCategoryNameQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<Result<CategoryNameResponse>> Handle(GetCategoryNameQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            name AS Name
        FROM categories
        WHERE id = @Id
        """;

        CategoryNameResponse? categoryNameResponse = await connection.QueryFirstOrDefaultAsync<CategoryNameResponse>(sql, new
        {
            request.Id
        });

        if(categoryNameResponse is null)
        {
            return Result.Failure<CategoryNameResponse>(CategoryErrors.NotFound);
        }

        return categoryNameResponse;
    }

}
