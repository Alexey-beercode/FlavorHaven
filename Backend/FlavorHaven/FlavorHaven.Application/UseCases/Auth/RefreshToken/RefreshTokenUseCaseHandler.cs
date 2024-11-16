using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.Providers.Interfaces;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Entities;
using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.RefreshToken;

public class RefreshTokenUseCaseHandler : IRequestHandler<RefreshTokenUseCase, TokensDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;

    public RefreshTokenUseCaseHandler(IUnitOfWork unitOfWork, ITokenProvider tokenProvider)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
    }

    public async Task<TokensDTO> Handle(RefreshTokenUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);
        if (user is null)
        {
            throw new AuthorizationException("Invalid refresh token.");
        }
        
        if (user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new AuthorizationException("Refresh token has expired.");
        }

        var tokenResponse = await GenerateTokensAsync(user, cancellationToken: cancellationToken);
        return tokenResponse;
    }
    
    private async Task<TokensDTO> GenerateTokensAsync(Domain.Entities.User user, CancellationToken cancellationToken = default)
    {
        var accessToken = await _tokenProvider.GenerateAccessTokenAsync(user, cancellationToken);
        
        return new TokensDTO()
        {
            AccessToken = accessToken.Token,
            RefreshToken = user.RefreshToken
        };
    }
}