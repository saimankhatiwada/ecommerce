using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.SubCategories.Shared;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Application.SubCategories.GetSubCategory;

internal sealed class GetSubCategoryQueryHandler : IQueryHandler<GetSubCategoryQuery, SubCategoryResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetSubCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<SubCategoryResponse>> Handle(GetSubCategoryQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            id AS Id,
            name AS Name,
            category_id AS CategoryId
        FROM sub_categories
        WHERE id = @Id
        """;

        SubCategoryResponse? subCategoryResponse = await connection.QueryFirstOrDefaultAsync<SubCategoryResponse>(sql, new
        {
            request.Id
        });

        if(subCategoryResponse is null)
        {
            return Result.Failure<SubCategoryResponse>(SubCategoryErrors.NotFound);
        }

        return subCategoryResponse;
    }

}
