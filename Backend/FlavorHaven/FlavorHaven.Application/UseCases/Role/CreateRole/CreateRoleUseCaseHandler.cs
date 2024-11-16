using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.CreateRole;

public class CreateRoleUseCaseHandler : IRequestHandler<CreateRoleUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateRoleUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateRoleUseCase request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Roles.GetByNameAsync(request.Name, cancellationToken);
        if (role is not null)
        {
            throw new EntityAlreadyExistsException(nameof(Domain.Entities.Role), request.Name);
        }

        role = _mapper.Map<Domain.Entities.Role>(request);

        await _unitOfWork.Roles.CreateAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}