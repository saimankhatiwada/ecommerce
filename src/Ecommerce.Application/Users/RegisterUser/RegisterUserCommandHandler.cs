using Ecommerce.Application.Abstractions.Authentication;
using Ecommerce.Application.Abstractions.Messaging;
using Ecommerce.Application.Abstractions.Storage;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Users;

namespace Ecommerce.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IBlobStorage _blobStorage;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        IBlobStorage blobStorage,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _blobStorage = blobStorage;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email),
            new MobileNumber(request.MobileNumber),
            new ImageName(request.ImageName),
            Role.FormRole(request.Role));

        string identityId = await _authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        _userRepository.Add(user);

        await _blobStorage.UploadAsync(request.ImageFileStream, request.ImageFileContentType, request.ImageName, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}
