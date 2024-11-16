using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Cart.GetCartsByUserId;

public class GetCartsByUserIdUseCaseHandler : IRequestHandler<GetCartsByUserIdUseCase, IEnumerable<CartDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCartsByUserIdUseCaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CartDTO>> Handle(GetCartsByUserIdUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.User), request.UserId);
        }

        var carts = await _unitOfWork.Cart.GetByUserId(request.UserId, cancellationToken);
        
        return _mapper.Map<IEnumerable<CartDTO>>(carts);
    }
}