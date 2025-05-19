using System.Linq.Expressions;
using EXE202_BE.Data.DTOS.User;
using AutoMapper;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Service.Services;

public class UserProfilesService : IUserProfilesService
{
    private readonly IUserProfilesRepository _userProfilesRepository;
    private readonly UserManager<ModifyIdentityUser> _userManager;
    private readonly IUserProfilesService _userProfilesService;
    private readonly IMapper _mapper;


    public UserProfilesService(
        IUserProfilesRepository userProfilesRepository,
        UserManager<ModifyIdentityUser> userManager,
        IMapper mapper)
    {
        _userProfilesRepository = userProfilesRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserProfiles> AddAsync(UserProfiles userProfile)
    {
        return await _userProfilesRepository.AddAsync(userProfile);
    }
    
    public async Task<UserProfileResponse> CreateUserAsync(CreateUserRequestDTO model)
    {
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

        await _userManager.AddToRoleAsync(user, model.Role);

        var userProfile = new UserProfiles
        {
            UserId = user.Id,
            FullName = model.FullName,
        };

        await _userProfilesRepository.AddAsync(userProfile);

        var dto = _mapper.Map<UserProfileResponse>(userProfile);
        dto.Role = model.Role;
        return dto;
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
            up => up.UPId == upId, "User,Allergies.Ingredient,PersonalHealthConditions.HealthCondition");

        if (userProfile == null)
        {
            throw new Exception("User profile not found.");
        }

        var user = await _userManager.FindByIdAsync(userProfile.UserId);
        var roles = await _userManager.GetRolesAsync(user);
        var dto = _mapper.Map<UserProfileResponse>(userProfile);
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
}