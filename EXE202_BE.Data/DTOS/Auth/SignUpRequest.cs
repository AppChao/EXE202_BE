namespace EXE202_BE.Data.DTOS.Auth;

public class SignUpRequest
{
    public string? email { get; set; }
    
    public string password { get; set; }

    public string role { get; set; }

    public int? weight { get; set; }

    public int? goalWeight { get; set; }

    public double? height { get; set; }

    public string? gender { get; set; }

    public int? age { get; set; }

    public int? goalId { get; set; }

    public MealScheduledDTO mealScheduledDTO { get; set; }

    public List<int>? listAllergies { get; set; }
    
    public List<int>? listHConditions { get; set; }

    public string deviceId { get; set; }
}

public class MealScheduledDTO
{
    public string BreakFastTime { get; set; }

    public string LunchTime { get; set; }

    public string DinnerTime { get; set; }
}