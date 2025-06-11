using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Service.Interface;

public interface IAllergiesService
{
    Task<List<Allergies>> CreateAllergies(UserProfiles info, SignUpRequest model);
}