using System.ComponentModel.DataAnnotations;

namespace Slot.Api.Controllers.Requests;

public class AuthReq
{
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}