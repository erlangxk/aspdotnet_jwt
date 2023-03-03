namespace Slot.Api.Conf;

public class JwtOptions
{
    public const string Name = "JWT";

    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecurityKey { get; set; } = null!;
}