using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.UpdateRole;

public class UpdateRoleUseCaseHandler : IRequestHandler<UpdateRoleUseCase>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateRoleUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateRoleUseCase request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Roles.GetByNameAsync(request.Name, cancellationToken);
        if (role is not null)
        {
            throw new EntityAlreadyExistsException(nameof(Domain.Entities.Role), request.Name);
        }
        
        role = await _unitOfWork.Roles.GetByIdAsync(request.Id, cancellationToken);
        if (role is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Role), request.Id);
        }

        _mapper.Map(request, role);
        
        _unitOfWork.Roles.Update(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}