namespace EXE202_BE.Data.DTOS.User;

public class UpdateUserProfileRequestDTO
{
    public string FullName { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Allergies { get; set; } = new List<string>();
    public List<HealthConditionDTO> HealthConditions { get; set; } = new List<HealthConditionDTO>();
}

public class HealthConditionDTO
{
    public string Condition { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}