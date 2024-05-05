using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Application.Products.GetProduct;
using Ecommerce.Application.Products.Shared;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Products;

namespace Ecommerce.Application.Products;

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private readonly IBlobStorage _blobStorage;

    public GetProductQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IBlobStorage blobStorage)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _blobStorage = blobStorage;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
        SELECT
            id AS Id,
            category_id AS CategoryId,
            sub_category_id AS SubCategoryId,
            name AS Name,
            description AS Description,
            status AS Status,
            money_amount AS Amount,
            money_currency AS Currency,
            image_name AS ImageUrl
        FROM products
        WHERE id = @Id
        """;

        ProductResponse? productResponse = await connection.QueryFirstOrDefaultAsync<ProductResponse>(sql, new
        {
            request.Id
        });

        if(productResponse is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);
        }

        Result<string> result = await _blobStorage.GetPresignedUrlAsync(productResponse.ImageUrl, cancellationToken);

        return productResponse with
        {
            ImageUrl = result.Value
        };
    }

}
