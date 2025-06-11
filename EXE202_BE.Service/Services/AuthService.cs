using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.DTOS.User;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EXE202_BE.Service.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ModifyIdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IUserProfilesService _userProfilesService;
    private readonly IUserProfilesRepository _userProfilesRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAllergiesService _allergiesService;
    private readonly IHealthConditionsService _healthConditionsService;
    private readonly IDevicesRepository _devicesRepository;
    private readonly IMealScheduledService _mealScheduledService;
    
    public AuthService(
        UserManager<ModifyIdentityUser> userManager,
        IConfiguration configuration,
        IUserProfilesService userProfilesService,
        IUserProfilesRepository userProfilesRepository,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthService> logger
        , IAllergiesService allergiesService
        , IHealthConditionsService healthConditionsService
        , IDevicesRepository devicesRepository
        , IMealScheduledService mealScheduledService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _userProfilesService = userProfilesService;
        _userProfilesRepository = userProfilesRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _allergiesService = allergiesService;
        _healthConditionsService = healthConditionsService;
        _devicesRepository = devicesRepository;
        _mealScheduledService = mealScheduledService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequestDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            throw new Exception("Invalid email or password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin") && !roles.Contains("Staff"))
        {
            throw new Exception("Your account is unauthorized.");
        }
        
        var userProfile = await _userProfilesRepository.GetAsync( 
            up => up.UserId == user.Id);

        if (userProfile == null)
        {
            throw new Exception("User profile not found.");
        }
        

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtSecurityToken(claims)),
            Role = roles.FirstOrDefault(),
            UPId = userProfile.UPId
        };
    }
    public async Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            _logger.LogInformation("Starting ChangePasswordAsync for request.");

            // Kiểm tra HttpContext
            if (_httpContextAccessor.HttpContext == null)
            {
                _logger.LogError("HttpContext is null in ChangePasswordAsync.");
                throw new InvalidOperationException("HttpContext is not available.");
            }

            // Lấy user ID từ token
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                _logger.LogWarning("Missing or invalid user ID claim in token.");
                throw new UnauthorizedAccessException("User not authenticated or missing user ID claim.");
            }
            var userId = userIdClaim.Value;
            _logger.LogInformation("Retrieved user ID: {UserId}", userId);

            // Tìm user
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User not found for ID: {UserId}", userId);
                throw new KeyNotFoundException("User not found.");
            }

            // Kiểm tra mật khẩu cũ
            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!passwordCheck)
            {
                _logger.LogInformation("Invalid current password for user ID: {UserId}", userId);
                throw new ArgumentException("Invalid current password.");
            }

            // Đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Failed to change password for user ID: {UserId}. Errors: {Errors}", userId, errors);
                throw new InvalidOperationException($"Failed to change password: {errors}");
            }

            _logger.LogInformation("Password changed successfully for user ID: {UserId}", userId);
        }

    public async Task<LoginResponse> CustomerLoginAsync(LoginRequestDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            throw new Exception("Invalid email or password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("Member") && !roles.Contains("User"))
        {
            throw new Exception("Your account is unauthorized.");
        }
        
        var userProfile = await _userProfilesRepository.GetAsync( 
            up => up.UserId == user.Id);
        if (userProfile == null)
        {
            throw new Exception("User profile not found. Please create an account");
        }

        // Logic tính streak
        var today = DateTime.UtcNow.Date;
        if (userProfile.LastLoginDate == null) // Lần đầu đăng nhập
        {
            userProfile.Streak = 1;
        }
        else
        {
            var lastLogin = userProfile.LastLoginDate.Value.Date;
            var daysDifference = (today - lastLogin).Days;

            if (daysDifference == 1) // Ngày liên tiếp
            {
                userProfile.Streak += 1;
            }
            else if (daysDifference > 1) // Không liên tiếp
            {
                userProfile.Streak = 1;
            }
            // Nếu daysDifference == 0 (đăng nhập trong cùng ngày), không thay đổi Streak
        }
        userProfile.LastLoginDate = today;
        await _userProfilesRepository.UpdateAsync(userProfile);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtSecurityToken(claims)),
            Role = roles.FirstOrDefault(),
            UPId = 2
        };
    }

    public async Task<LoginResponse> LoginGoogleAsync(LoginGoogleRequest model)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(model.idToken);

        var user = await _userManager.FindByEmailAsync(payload.Email);

        if (user == null)
        {
            user = new ModifyIdentityUser()
            {
                UserName = payload.Email,
                Email = payload.Email,
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(user);
        }
        
        await _userManager.AddToRoleAsync(user, "User");

        var userProfile = await _userProfilesRepository.GetAsync(up => up.UserId == user.Id);
        if (userProfile == null)
        {
            throw new Exception("User profile not found. Please create an account");
        }

        // Logic tính streak
        var today = DateTime.UtcNow.Date;
        if (userProfile.LastLoginDate == null)
        {
            userProfile.Streak = 1;
        }
        else
        {
            var lastLogin = userProfile.LastLoginDate.Value.Date;
            var daysDifference = (today - lastLogin).Days;

            if (daysDifference == 1)
            {
                userProfile.Streak += 1;
            }
            else if (daysDifference > 1)
            {
                userProfile.Streak = 1;
            }
        }
        userProfile.LastLoginDate = today;
        await _userProfilesRepository.UpdateAsync(userProfile);
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtSecurityToken(claims)),
            Role = roles.FirstOrDefault(),
            UPId = 1
        };    
    }

    public JwtSecurityToken GenerateJwtSecurityToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(29),
            signingCredentials: creds);
        
        return token;
    }

    public async Task<SignUpResponse> SignUp(SignUpRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.email);
        if (user != null)
        {
            throw new Exception("The email address already exists");
        }

        var newUser = new ModifyIdentityUser()
        {
            Email = model.email,
            UserName = model.email,
        };
        
        var result = await _userManager.CreateAsync(newUser, model.password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        
        await _userManager.AddToRoleAsync(newUser, model.role);

        var newUserProfile = await _userProfilesService.CreateUserProfilesAsync(model, newUser);

        if (model.listAllergies != null && model.listAllergies.Any())
        {
            await _allergiesService.CreateAllergies(newUserProfile, model);
        }

        if (model.listHConditions != null && model.listHConditions.Any())
        { 
            await _healthConditionsService.CreateHealthConditions(newUserProfile, model);
        }

        await _devicesRepository.CreateDeviceToken(newUser.Id, model.deviceId);
        
        await _mealScheduledService.CreateMealScheduled(newUserProfile.UPId, model);

        var roles = await _userManager.GetRolesAsync(newUser);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, newUser.Id),
            new Claim(ClaimTypes.Email, newUser.Email)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        return new SignUpResponse
        {
            UPId = newUserProfile.UPId,
            JWTToken = new JwtSecurityTokenHandler().WriteToken(GenerateJwtSecurityToken(claims)),
            Role = model.role,
        };
    }
}