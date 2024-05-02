using System.Data;
using Dapper;
using Ecommerce.Application.Abstractions.Authentication;
using Ecommerce.Application.Abstractions.Data;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Application.Users.shared;
using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;
    private readonly IBlobStorage _blobStorage;

    public GetLoggedInUserQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IUserContext userContext,
        IBlobStorage blobStorage)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
        _blobStorage = blobStorage;
    }

    public async Task<Result<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                email AS Email,
                first_name AS FirstName,
                last_name AS LastName,
                mobile_number AS MobileNumber,
                image_name AS ImageUrl
            FROM users
            WHERE identity_id = @IdentityId
            """;

        UserResponse userResponse = await connection.QuerySingleAsync<UserResponse>(
            sql,
            new
            {
                _userContext.IdentityId
            });

        Result<string> result = await _blobStorage.GetPresignedUrlAsync(userResponse.ImageUrl, cancellationToken);

        return new UserResponse
        {
            Id = userResponse.Id,
            Email = userResponse.Email,
            FirstName = userResponse.FirstName,
            LastName = userResponse.LastName,
            ImageUrl = result.Value
        };
    }
}
