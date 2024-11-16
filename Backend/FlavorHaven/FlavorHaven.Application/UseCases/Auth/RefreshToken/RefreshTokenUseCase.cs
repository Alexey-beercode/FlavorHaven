using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.RefreshToken;

public class RefreshTokenUseCase : IRequest<TokensDTO>
{
    public string RefreshToken { get; set; }
}