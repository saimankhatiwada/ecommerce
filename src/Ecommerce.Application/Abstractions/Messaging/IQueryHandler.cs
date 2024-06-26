using Ecommerce.Domain.Abstractions;
using MediatR;

namespace Ecommerce.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
