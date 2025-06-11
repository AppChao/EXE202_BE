using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace EXE202_BE.Service.Services;

public class AllergiesService : IAllergiesService
{
    private readonly IAllergiesRepository _repository;
    public AllergiesService(IAllergiesRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Allergies>> CreateAllergies(UserProfiles info, SignUpRequest model)
    {
        List<Allergies> allergies = new List<Allergies>();
        foreach (var item in model.listAllergies)
        {
            var newAller = new Allergies
            {
                IngredientId = item,
                UPId = info.UPId
            };
            
            await _repository.AddAsync(newAller);
            allergies.Add(newAller);
        }
        return allergies;
    }
}