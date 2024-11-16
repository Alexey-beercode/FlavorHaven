using FlavorHaven.Application.Models.Results;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Application.Providers.Interfaces;

public interface ITokenProvider
{
    Task<TokenResult> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken = default);
    TokenResult GenerateRefreshToken();
}