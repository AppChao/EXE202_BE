namespace EXE202_BE.Data.DTOS.Auth;

public class SignUpResponse
{
    public string? JWTToken { get; set; }
    
    public int? UPId { get; set; }

    public string? Role { get; set; }
}