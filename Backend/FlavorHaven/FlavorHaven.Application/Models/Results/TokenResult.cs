namespace FlavorHaven.Application.Models.Results;

public class TokenResult
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}