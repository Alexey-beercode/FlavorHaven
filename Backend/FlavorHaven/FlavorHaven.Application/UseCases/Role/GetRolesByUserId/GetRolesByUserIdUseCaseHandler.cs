using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.GetRolesByUserId;

public class GetRolesByUserIdUseCaseHandler : IRequestHandler<GetRolesByUserIdUseCase, IEnumerable<RoleDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRolesByUserIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDTO>> Handle(GetRolesByUserIdUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.User), request.UserId);
        }

        var roles = await _unitOfWork.Roles.GetRolesByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<RoleDTO>>(roles);
    }
}