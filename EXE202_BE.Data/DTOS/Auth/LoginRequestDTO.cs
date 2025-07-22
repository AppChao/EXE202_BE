namespace EXE202_BE.Data.DTOS.Auth;

public class LoginRequestDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string deviceToken { get; set; }
}