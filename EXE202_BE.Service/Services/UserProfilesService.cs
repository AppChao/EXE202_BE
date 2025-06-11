using System.Linq.Expressions;
using EXE202_BE.Data.DTOS.User;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EXE202_BE.Service.Services;

public class UserProfilesService : IUserProfilesService
{
    private readonly IUserProfilesRepository _userProfilesRepository;
    private readonly IAllergiesRepository _allergiesRepository;
    private readonly IPersonalHealthConditionsRepository _personalHealthConditionsRepository;
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IHealthConditionsRepository _healthConditionsRepository;
    private readonly UserManager<ModifyIdentityUser> _userManager;
    private readonly IUserProfilesService _userProfilesService;
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;
    private readonly Cloudinary _cloudinary;
    private readonly ILogger<UserProfilesService> _logger;



    public UserProfilesService(
        IUserProfilesRepository userProfilesRepository,
        IAllergiesRepository allergiesRepository,
        IPersonalHealthConditionsRepository personalHealthConditionsRepository,
        IIngredientsRepository ingredientsRepository,
        IHealthConditionsRepository healthConditionsRepository,
        UserManager<ModifyIdentityUser> userManager,
        IMapper mapper,
        AppDbContext dbContext,
        Cloudinary cloudinary,
        ILogger<UserProfilesService> logger)
    {
        _userProfilesRepository = userProfilesRepository;
        _userManager = userManager;
        _mapper = mapper;
        _dbContext = dbContext;
        _cloudinary = cloudinary;
        _allergiesRepository = allergiesRepository;
        _personalHealthConditionsRepository = personalHealthConditionsRepository;
        _ingredientsRepository = ingredientsRepository;
        _healthConditionsRepository = healthConditionsRepository;
        _logger = logger;
    }

    public async Task<UserProfiles> AddAsync(UserProfiles userProfile)
    {
        return await _userProfilesRepository.AddAsync(userProfile);
    }
    
    public async Task<UserProfileResponse> CreateUserAsync(CreateUserRequestDTO model)
    {
        try
        {
            // Tạo Identity User
            var user = new ModifyIdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Gán vai trò
            await _userManager.AddToRoleAsync(user, model.Role);

            // Tạo UserProfile
            var userProfile = new UserProfiles
            {
                UserId = user.Id,
                FullName = model.FullName,
                Gender = "Other"
            };

            await _userProfilesRepository.AddAsync(userProfile);

            var dto = _mapper.Map<UserProfileResponse>(userProfile);
            dto.Role = model.Role;
            return dto;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception($"Failed to create user profile: {ex.InnerException?.Message ?? ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create user: {ex.Message}", ex);
        }
    }

    public async Task<PageListResponse<UserProfileResponse>> GetUsersAsync(
        string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        Expression<Func<UserProfiles, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = up => up.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                          up.User.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

        var userProfiles = await _userProfilesRepository.GetAllAsync(
            filter, "User,Allergies.Ingredient,PersonalHealthConditions.HealthCondition");

        var totalCount = userProfiles.Count();

        var paginatedUsers = userProfiles
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new List<UserProfileResponse>();
        foreach (var up in paginatedUsers)
        {
            var user = await _userManager.FindByIdAsync(up.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var dto = _mapper.Map<UserProfileResponse>(up);
            dto.Role = roles.FirstOrDefault();
            result.Add(dto);
        }

        return new PageListResponse<UserProfileResponse>
        {
            Items = result,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }

    public async Task<UserProfileResponse> GetUserProfileAsync(int upId)
    {
        var userProfile = await _userProfilesRepository.GetAsync(
            up => up.UPId == upId, "User,PersonalHealthConditions.HealthCondition");
        if (userProfile == null)
        {
            throw new Exception("User profile not found.");
        }

        var allergies = await _dbContext.Allergies
            .Where(a => a.UPId == upId)
            .Include(a => a.Ingredient)
            .Select(a => a.Ingredient.IngredientName)
            .ToListAsync();

        var user = await _userManager.FindByIdAsync(userProfile.UserId);
        var roles = await _userManager.GetRolesAsync(user);
        var dto = _mapper.Map<UserProfileResponse>(userProfile);
        dto.Allergies = allergies;
        dto.Role = roles.FirstOrDefault();
        return dto;
    }
    
    public async Task<AdminProfileResponse> GetAdminProfileAsync(int upId)
    {
        var userProfile = await _userProfilesRepository.GetAsync(
            up => up.UPId == upId, "User");

        if (userProfile == null)
        {
            throw new Exception("User profile not found.");
        }

        var user = await _userManager.FindByIdAsync(userProfile.UserId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var dto = _mapper.Map<AdminProfileResponse>(userProfile);
        dto.Role = roles.FirstOrDefault();
        return dto;
    }

    public async Task<AdminProfileResponse> UpdateAdminProfileAsync(int upId, AdminProfileResponse request)
    {
        var userProfile = await _userProfilesRepository.GetAsync(
            up => up.UPId == upId, "User");

        if (userProfile == null)
        {
            throw new Exception("User profile not found.");
        }

        var user = await _userManager.FindByIdAsync(userProfile.UserId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        if (upId != request.UPId)
        {
            throw new Exception("UPId mismatch.");
        }

        user.UserName = request.Username ?? user.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        userProfile.FullName = request.FullName;

        await _userProfilesRepository.UpdateAsync(userProfile);

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new Exception("Failed to update user: " + string.Join(", ", updateResult.Errors.Select(e => e.Description)));
        }

        var roles = await _userManager.GetRolesAsync(user);
        var dto = _mapper.Map<AdminProfileResponse>(userProfile);
        dto.Role = roles.FirstOrDefault();
        return dto;
    }
    
    public async Task<ProfileImageResponseDTO> UploadProfileImageAsync(int upId, IFormFile image)
    {
        if (image == null || !new[] { "image/jpeg", "image/png", "image/gif" }.Contains(image.ContentType))
            throw new ArgumentException("Invalid file type. Only JPEG, PNG, or GIF allowed.");

        if (image.Length > 5 * 1024 * 1024) // 5MB limit
            throw new ArgumentException("File size exceeds 5MB limit.");

        var userProfile = await _userProfilesRepository.GetAsync(up => up.UPId == upId, "User");
        if (userProfile == null)
            throw new Exception("User profile not found.");

        // Upload to Cloudinary
        using var stream = image.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(image.FileName, stream),
            Folder = $"user_profiles/{upId}",
            Transformation = new Transformation().Width(100).Height(100).Crop("fill").Quality(80)
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
            throw new Exception($"Upload failed: {uploadResult.Error.Message}");

        // Update UserPicture
        userProfile.UserPicture = new Uri(uploadResult.SecureUrl.ToString());
        await _userProfilesRepository.UpdateAsync(userProfile);

        // Return the URL as a string
        return new ProfileImageResponseDTO { SecureUrl = uploadResult.SecureUrl.ToString() };
    }
    
    public async Task<UserProfileResponse> UpdateUserProfileAsync(int upId, UpdateUserProfileRequestDTO model)
    {
        // Validate inputs
        if (string.IsNullOrEmpty(model.FullName))
            throw new Exception("FullName is required.");
        if (!new[] { "Male", "Female", "Other" }.Contains(model.Gender))
            throw new Exception("Invalid Gender. Must be 'Male', 'Female', or 'Other'.");
        if (string.IsNullOrEmpty(model.Email) || !model.Email.Contains("@"))
            throw new Exception("Invalid Email.");

        // Start transaction
        _logger.LogInformation("Starting transaction for UPId: {UPId}", upId);
        using var transaction = await _userProfilesRepository.GetDbContext().Database.BeginTransactionAsync();
        _logger.LogInformation("Transaction started with ID: {TransactionId}. DbContext instance: {DbContextId}", 
            transaction.TransactionId, _userProfilesRepository.GetDbContext().GetHashCode());

        try
        {
            // Get existing profile
            _logger.LogInformation("Fetching user profile for UPId: {UPId}", upId);
            var userProfile = await _userProfilesRepository.GetAsync(p => p.UPId == upId, "User");
            if (userProfile == null)
                throw new Exception("User profile not found.");

            // Update UserProfiles
            _logger.LogInformation("Mapping DTO to UserProfiles. DbContext instance: {DbContextId}", 
                _userProfilesRepository.GetDbContext().GetHashCode());
            _mapper.Map(model, userProfile);
            _logger.LogInformation("Updating user profile...");
            await _userProfilesRepository.UpdateAsync(userProfile);

            // Update AspNetUsers
            _logger.LogInformation("Fetching identity user for UserId: {UserId}", userProfile.UserId);
            var identityUser = await _userManager.FindByIdAsync(userProfile.UserId);
            if (identityUser == null)
                throw new Exception("Identity user not found.");
            if (identityUser.Email != model.Email)
            {
                _logger.LogInformation("Updating email for identity user...");
                identityUser.Email = model.Email;
                identityUser.NormalizedEmail = model.Email.ToUpper();
                var updateResult = await _userManager.UpdateAsync(identityUser);
                if (!updateResult.Succeeded)
                    throw new Exception(string.Join(", ", updateResult.Errors.Select(e => e.Description)));
            }

            // Update Allergies and PersonalHealthConditions
            _logger.LogInformation("Updating allergies for UPId: {UPId}. DbContext instance: {DbContextId}", 
                upId, _allergiesRepository.GetDbContext().GetHashCode());
            await _userProfilesRepository.UpdateAllergiesAsync(upId, model.Allergies ?? new List<string>());

            _logger.LogInformation("Updating health conditions for UPId: {UPId}. DbContext instance: {DbContextId}", 
                upId, _personalHealthConditionsRepository.GetDbContext().GetHashCode());
            await _userProfilesRepository.UpdatePersonalHealthConditionsAsync(upId, model.HealthConditions ?? new List<HealthConditionDTO>());

            // Commit transaction
            _logger.LogInformation("Committing transaction...");
            await transaction.CommitAsync();

            // Get updated profile
            _logger.LogInformation("Fetching updated profile for UPId: {UPId}", upId);
            var updatedProfile = await _userProfilesRepository.GetAsync(p => p.UPId == upId, "User,PersonalHealthConditions.HealthCondition");
            if (updatedProfile == null)
                throw new Exception("Updated profile not found.");

            var allergies = await _userProfilesRepository.GetDbContext().Allergies
                .Where(a => a.UPId == upId)
                .Include(a => a.Ingredient)
                .Select(a => a.Ingredient.IngredientName)
                .ToListAsync();

            var response = _mapper.Map<UserProfileResponse>(updatedProfile);
            response.Allergies = allergies;
            response.Email = identityUser.Email;

            // Gán Role
            _logger.LogInformation("Fetching roles for identity user...");
            var roles = await _userManager.GetRolesAsync(identityUser);
            response.Role = roles.FirstOrDefault();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during transaction for UPId: {UPId}", upId);
            await transaction.RollbackAsync();
            throw new Exception($"Failed to update user profile: {ex.Message}", ex);
        }
    }

    public async Task<UserProfiles> CreateUserProfilesAsync(SignUpRequest model, ModifyIdentityUser modifyIdentityUser)
    {
        var newUP = new UserProfiles
        {
            GoalId = model.goalId,
            Weight = model.weight,
            GoalWeight = model.goalWeight,
            Height = model.height,
            Gender = model.gender,
            Age = model.age,
            UserId = modifyIdentityUser.Id
        };
        
        return await _userProfilesRepository.AddAsync(newUP);
    }
}