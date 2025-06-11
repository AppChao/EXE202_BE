using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.DTOS.User;

namespace EXE202_BE.Service.Interface;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequestDTO model);
    Task ChangePasswordAsync(ChangePasswordRequest request);
    
    Task<LoginResponse> CustomerLoginAsync(LoginRequestDTO model);
    
    Task<LoginResponse> LoginGoogleAsync(LoginGoogleRequest model);
    
    JwtSecurityToken GenerateJwtSecurityToken(List<Claim> claims);

    Task<SignUpResponse> SignUp(SignUpRequest model);
}