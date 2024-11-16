using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FlavorHaven.Application.Models.Options;
using FlavorHaven.Application.Models.Results;
using FlavorHaven.Application.Providers.Interfaces;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FlavorHaven.Application.Providers.Implementation;

public class TokenProvider : ITokenProvider
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenOptions _tokenOptions;

    public TokenProvider(IUnitOfWork unitOfWork, IOptions<TokenOptions> tokenOptions)
    {
        _unitOfWork = unitOfWork;
        _tokenOptions = tokenOptions.Value;
    }

    public async Task<TokenResult> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var claims = await GetClaimsAsync(user, cancellationToken);
        
        var signingCredentials = GetSigningCredentials();
        var tokenResult = GenerateToken(signingCredentials, claims);
        
        return tokenResult;
    }

    public TokenResult GenerateRefreshToken()
    {
        int size = 64;
        var randomNumber = new byte[size];
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        var tokenResult = new TokenResult()
        {
            Token = Convert.ToBase64String(randomNumber),
            Expiration = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenValidityInDays)
        };

        return tokenResult;
    }
    
    private async Task<List<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName)
        };

        var roles = await _unitOfWork.Roles.GetRolesByUserIdAsync(user.Id, cancellationToken);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_tokenOptions.SecretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private TokenResult GenerateToken(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var expires = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenValidityInMinutes);
        
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);;

        var tokenResult = new TokenResult()
        {
            Token = accessToken,
            Expiration = expires
        };
        
        return tokenResult;
    }
}