namespace EXE202_BE.Data.DTOS.User;

public class UserProfileResponse
{
    public int UPId { get; set; }
    public string FullName { get; set; }
    public int? SubscriptionId { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}