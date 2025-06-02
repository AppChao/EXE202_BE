namespace EXE202_BE.Data.DTOS;

public class HealthConditionResponse
{
    public int HealthConditionId { get; set; }
    public string HealthConditionName { get; set; } = string.Empty;
    public string BriefDescription { get; set; } = string.Empty;
}