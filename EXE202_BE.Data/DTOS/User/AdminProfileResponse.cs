namespace EXE202_BE.Data.DTOS.User;

public class AdminProfileResponse
{
    public int UPId { get; set; }
    public string FullName { get; set; }
    public string? Username { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public string? UserPicture { get; set; }
}