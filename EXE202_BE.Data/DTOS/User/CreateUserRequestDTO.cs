namespace EXE202_BE.Data.DTOS.User;

public class CreateUserRequestDTO
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
}
    