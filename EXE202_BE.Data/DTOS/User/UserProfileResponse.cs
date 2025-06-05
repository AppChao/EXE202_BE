namespace EXE202_BE.Data.DTOS.User;

public class UserProfileResponse
{
    public int UPId { get; set; }
    public string FullName { get; set; }
    public string? Username { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public List<string> Allergies { get; set; }
    public List<HealthConditionDTO> HealthConditions { get; set; } = new List<HealthConditionDTO>();    public string UserId { get; set; }
    public string Email { get; set; }
    public string? Role { get; set; }
    public string? UserPicture { get; set; }
    public string? PhoneNumber { get; set; }
    public int? SubcriptionId  { get; set; }
    public DateTime? EndDate  { get; set; }
    public int Streak { get; set; }
}