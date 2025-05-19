using System.ComponentModel.DataAnnotations;

namespace EXE202_BE.Data.DTOS.Auth;

public class ChangePasswordRequest
{
    [Required(ErrorMessage = "Current password is required")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "New password is required")]
    [MinLength(8, ErrorMessage = "New password must be at least 8 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "New password must contain at least one uppercase letter, one digit, and one special character")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("NewPassword", ErrorMessage = "New password and confirm password do not match")]
    public string ConfirmPassword { get; set; }
}