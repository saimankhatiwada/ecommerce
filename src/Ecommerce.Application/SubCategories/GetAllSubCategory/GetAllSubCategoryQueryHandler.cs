using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.SubCategories.Shared;
using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Application.SubCategories.GetAllSubCategory;

internal sealed class GetAllSubCategoryQueryHandler : IQueryHandler<GetAllSubCategoryQuery, IReadOnlyList<SubCategoryResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllSubCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<SubCategoryResponse>>> Handle(GetAllSubCategoryQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            id AS Id,
            name AS Name,
            category_id AS CategoryId
        FROM sub_categories
        """;

        IEnumerable<SubCategoryResponse> subCategoryResponses = await connection.QueryAsync<SubCategoryResponse>(sql, cancellationToken);

        return subCategoryResponses.ToList().AsReadOnly();
    }

}
