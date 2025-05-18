using System.Linq.Expressions;
using EXE202_BE.Data.DTOS.User;
using AutoMapper;
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

    public async Task<List<UserProfileResponse>> GetUsersAsync(string? searchTerm)
    {
        // Tạo filter (LINQ expression)
        Expression<Func<UserProfiles, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = up => up.FullName.Contains(searchTerm) || up.User.Email.Contains(searchTerm);
        }

        // Gọi repository và include navigation property "User"
        var userProfiles = await _userProfilesRepository.GetAllAsync(filter, "User");

        // Chuẩn bị kết quả
        var result = new List<UserProfileResponse>();

        foreach (var up in userProfiles)
        {
            var user = await _userManager.FindByIdAsync(up.UserId);
            var roles = await _userManager.GetRolesAsync(user);

            var dto = _mapper.Map<UserProfileResponse>(up);
            dto.Role = roles.FirstOrDefault();

            result.Add(dto);
        }

        return result;
    }

    public async Task<UserProfileResponse> GetUserByIdAsync(int upId)
    {
        var userProfile = await _userProfilesRepository.GetAsync(up => up.UPId == upId, "User");
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

    public async Task<UserProfileResponse> EditAsync(int upId, EditUserRequestDTO model)
    {
        var userProfile = await _userProfilesRepository.GetAsync(up => up.UPId == upId, "User");
        if (userProfile == null)
        {
            throw new Exception("User profile not found.");
        }

        userProfile.FullName = model.FullName;

        await _userProfilesRepository.UpdateAsync(userProfile);

        var dto = _mapper.Map<UserProfileResponse>(userProfile);

        // Nếu vẫn muốn trả role ra (chứ không update), lấy từ user
        var user = await _userManager.FindByIdAsync(userProfile.UserId);
        var roles = await _userManager.GetRolesAsync(user);
        dto.Role = roles.FirstOrDefault();

        return dto;
    }

}