using System.ComponentModel.DataAnnotations;

namespace Slot.Api.Controllers.Requests;

public class Registration
{
    [Required] public string Email { get; set; } = null!;
    [Required] public string Username { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;

}