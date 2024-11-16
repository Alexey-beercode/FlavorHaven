using AutoMapper;
using FlavorHaven.Application.Exceptions;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.Providers.Interfaces;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Entities;
using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.Login;

public class LoginUseCaseHandler : IRequestHandler<LoginUseCase, TokensDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordProvider _passwordProvider;
    private readonly IMapper _mapper;

    public LoginUseCaseHandler(
        IUnitOfWork unitOfWork,
        ITokenProvider tokenProvider,
        IPasswordProvider passwordProvider)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _passwordProvider = passwordProvider;
    }

    public async Task<TokensDTO> Handle(LoginUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByNameAsync(request.UserName, cancellationToken);
        if (user is null)
        {
            throw new AuthorizationException("Login or password entered incorrectly.");
        }

        var isPasswordCorrect = _passwordProvider.VerifyPassword(user.PasswordHash, request.Password);
        if (!isPasswordCorrect)
        {
            throw new AuthorizationException("Login or password entered incorrectly.");
        }
        
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