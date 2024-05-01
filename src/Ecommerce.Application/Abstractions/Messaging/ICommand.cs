using Ecommerce.Domain.Abstractions;
using MediatR;

namespace Ecommerce.Application.Abstractions.Messaging;


public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}
