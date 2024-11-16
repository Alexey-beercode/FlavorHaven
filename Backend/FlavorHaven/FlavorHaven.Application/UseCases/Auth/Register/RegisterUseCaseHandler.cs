using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.Providers.Interfaces;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Entities;
using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.Register;

public class RegisterUseCaseHandler : IRequestHandler<RegisterUseCase, TokensDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordProvider _passwordProvider;
    private readonly IMapper _mapper;

    public RegisterUseCaseHandler(
        IUnitOfWork unitOfWork,
        ITokenProvider tokenProvider,
        IPasswordProvider passwordProvider,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _passwordProvider = passwordProvider;
        _mapper = mapper;
    }

    public async Task<TokensDTO> Handle(RegisterUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByNameAsync(request.UserName, cancellationToken);
        if (user is not null)
        {
            throw new AuthorizationException("User with this login already exists.");
        }

        user = _mapper.Map<User>(request);
        user.PasswordHash = _passwordProvider.HashPassword(request.Password);
        
        await _unitOfWork.Users.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userRole = await _unitOfWork.Roles.GetByNameAsync("Resident", cancellationToken);
        if (userRole is null)
        {
            throw new EntityNotFoundException(nameof(Role), "Resident");
        }

        await _unitOfWork.Roles.SetRoleToUserAsync(user.Id, userRole.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var tokenResponse = await GenerateTokensAsync(user, cancellationToken);
        return tokenResponse;
    }
    
    private async Task<TokensDTO> GenerateTokensAsync(User user, CancellationToken cancellationToken = default)
    {
        var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user, cancellationToken);

        var refreshToken = _tokenProvider.GenerateRefreshToken();

        user.RefreshToken = refreshToken.Token;
        user.RefreshTokenExpiryTime = refreshToken.Expiration;
        
        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new TokensDTO()
        {
            AccessToken = accessToken.Token,
            RefreshToken = user.RefreshToken
        };
    }
}