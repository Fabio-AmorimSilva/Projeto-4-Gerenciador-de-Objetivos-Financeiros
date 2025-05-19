namespace FinancialGoalsManager.Infrastructure.Auth.Services;

public sealed class TokenService(IOptions<JwtSettings> options) : ITokenService
{
    private readonly JwtSettings _settings = options.Value;

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.JwtKey ?? string.Empty);
        var claims = user.GetClaims();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_settings.ExpireMinutes),
            Issuer = _settings.Emissary,
            Audience = _settings.ValidOn,
            SigningCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}