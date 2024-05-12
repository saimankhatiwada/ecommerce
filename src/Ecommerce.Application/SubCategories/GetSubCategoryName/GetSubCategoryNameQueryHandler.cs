using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.SubCategories.Shared;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.SubCategories;

namespace Ecommerce.Application.SubCategories.GetSubCategoryName;

internal sealed class GetSubCategoryNameQueryHandler : IQueryHandler<GetSubCategoryNameQuery, SubCategoryNameResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetSubCategoryNameQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<Result<SubCategoryNameResponse>> Handle(GetSubCategoryNameQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            name AS Name
        FROM sub_categories
        WHERE id = @Id
        """;

        SubCategoryNameResponse? subCategoryNameResponse = await connection.QueryFirstOrDefaultAsync<SubCategoryNameResponse>(sql, new
        {
            request.Id
        });

        if(subCategoryNameResponse is null)
        {
            return Result.Failure<SubCategoryNameResponse>(SubCategoryErrors.NotFound);
        }

        return subCategoryNameResponse;
    }

}
