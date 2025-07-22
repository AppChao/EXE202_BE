namespace EXE202_BE.Data.DTOS.Auth;

public class LoginGoogleRequest
{
    public string idToken { get; set; }
    
    public string deviceToken { get; set; }
}