using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.Register;

public class RegisterUseCase : IRequest<TokensDTO>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}