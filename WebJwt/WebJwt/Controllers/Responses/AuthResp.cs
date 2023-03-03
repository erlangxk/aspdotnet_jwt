namespace Slot.Api.Controllers.Responses;

public class AuthResp
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}