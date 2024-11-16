using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.GetRoleByName;

public class GetRoleByNameUseCaseHandler : IRequestHandler<GetRoleByNameUseCase, RoleDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoleByNameUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RoleDTO> Handle(GetRoleByNameUseCase request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Roles.GetByNameAsync(request.Name, cancellationToken);
        if (role is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Role), $"name: {request.Name}");
        }

        return _mapper.Map<RoleDTO>(role);
    }
}