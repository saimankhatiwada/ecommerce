using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Application.Products.Shared;
using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Application.Products.GetAllProduct;

internal sealed class GetAllProductQueryHandler : IQueryHandler<GetAllProductQuery, IReadOnlyList<ProductResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private readonly IBlobStorage _blobStorage;

    public GetAllProductQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IBlobStorage blobStorage)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _blobStorage = blobStorage;
    }

    public async Task<Result<IReadOnlyList<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
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
        """;

        IEnumerable<ProductResponse> productsResponses = await connection.QueryAsync<ProductResponse>(sql, cancellationToken);

        return (await Task.WhenAll(productsResponses.Select(async product =>
        {
            Result<string> result = await _blobStorage.GetPresignedUrlAsync(product.ImageUrl, cancellationToken);

            return product with
            {
                ImageUrl = result.Value
            };
        }))).ToList().AsReadOnly();
    }

}
