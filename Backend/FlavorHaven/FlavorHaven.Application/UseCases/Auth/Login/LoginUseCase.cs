using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.Login;

public class LoginUseCase : IRequest<TokensDTO>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}