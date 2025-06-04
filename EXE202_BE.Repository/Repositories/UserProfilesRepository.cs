using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.User;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace EXE202_BE.Repository.Repositories;

public class UserProfilesRepository : GenericRepository<UserProfiles>, IUserProfilesRepository
{
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IAllergiesRepository _allergiesRepository;
    private readonly IHealthConditionsRepository _healthConditionsRepository;
    private readonly IPersonalHealthConditionsRepository _personalHealthConditionsRepository;
    private readonly ILogger<UserProfilesRepository> _logger;


    public UserProfilesRepository(
        AppDbContext db,
        IIngredientsRepository ingredientsRepository,
        IAllergiesRepository allergiesRepository,
        IHealthConditionsRepository healthConditionsRepository,
        IPersonalHealthConditionsRepository personalHealthConditionsRepository,
        ILogger<UserProfilesRepository> logger)
        : base(db)
    {
        _ingredientsRepository = ingredientsRepository;
        _allergiesRepository = allergiesRepository;
        _healthConditionsRepository = healthConditionsRepository;
        _personalHealthConditionsRepository = personalHealthConditionsRepository;
        _logger = logger;
    }

    public async Task UpdateAllergiesAsync(int upId, List<string> newAllergies)
    {
        _logger.LogInformation("Updating allergies for UPId: {UPId} with new allergies: {Allergies}", 
            upId, string.Join(", ", newAllergies));

        var currentAllergies = (await _allergiesRepository.GetAllAsync(a => a.UPId == upId, "Ingredient"))
            .Select(a => new { a.IngredientId, a.Ingredient.IngredientName }).ToList();
        _logger.LogInformation("Current allergies: {CurrentAllergies}", 
            string.Join(", ", currentAllergies.Select(a => a.IngredientName)));

        // Remove old allergies
        foreach (var allergy in currentAllergies.Where(ca => !newAllergies.Contains(ca.IngredientName, StringComparer.OrdinalIgnoreCase)))
        {
            _logger.LogInformation("Removing allergy IngredientId: {IngredientId} for UPId: {UPId}", 
                allergy.IngredientId, upId);
            var allergyEntity = await _allergiesRepository.GetAsync(a => a.UPId == upId && a.IngredientId == allergy.IngredientId);
            if (allergyEntity != null)
                await _allergiesRepository.DeleteAsync(allergyEntity);
        }

        // Load only relevant ingredients for case-insensitive comparison
        var allIngredients = await _ingredientsRepository.GetAllAsync(i => newAllergies.Contains(i.IngredientName));

        // Add new allergies
        foreach (var allergy in newAllergies.Except(currentAllergies.Select(ca => ca.IngredientName), StringComparer.OrdinalIgnoreCase))
        {
            _logger.LogInformation("Adding new allergy: {AllergyName} for UPId: {UPId}", allergy, upId);
            var ingredient = allIngredients.FirstOrDefault(i => i.IngredientName.Equals(allergy, StringComparison.OrdinalIgnoreCase));
            if (ingredient == null)
            {
                // Try case-insensitive search in case the exact match failed
                ingredient = allIngredients.FirstOrDefault(i => i.IngredientName.ToLower() == allergy.ToLower());
                if (ingredient == null)
                {
                    _logger.LogWarning("Ingredient '{Allergy}' not found.", allergy);
                    continue; // Bỏ qua nếu không tìm thấy ingredient
                }
            }
            var newAllergy = new Allergies
            {
                UPId = upId,
                IngredientId = ingredient.IngredientId
            };
            await _allergiesRepository.AddAsync(newAllergy);
        }
    }

    public async Task UpdatePersonalHealthConditionsAsync(int upId, List<HealthConditionDTO> newConditions)
    {
        _logger.LogInformation("Updating health conditions for UPId: {UPId}. DbContext instance: {DbContextId}", 
            upId, _personalHealthConditionsRepository.GetDbContext().GetHashCode());

        var currentConditions = (await _personalHealthConditionsRepository.GetAllAsync(phc => phc.UPId == upId, "HealthCondition"))
            .Select(phc => new { phc.HealthConditionId, phc.HealthCondition.HealthConditionName, phc.Status }).ToList();
        _logger.LogInformation("Fetched {Count} current health conditions for UPId: {UPId}", currentConditions.Count, upId);

        // Remove old conditions
        foreach (var condition in currentConditions.Where(c => !newConditions.Any(nc => nc.Condition == c.HealthConditionName)))
        {
            _logger.LogInformation("Removing health condition HealthConditionId: {HealthConditionId} for UPId: {UPId}", 
                condition.HealthConditionId, upId);
            var conditionEntity = await _personalHealthConditionsRepository.GetAsync(phc => phc.UPId == upId && phc.HealthConditionId == condition.HealthConditionId);
            if (conditionEntity != null)
                await _personalHealthConditionsRepository.DeleteAsync(conditionEntity);
        }

        // Add or update conditions
        foreach (var condition in newConditions)
        {
            _logger.LogInformation("Processing health condition: {Condition} for UPId: {UPId}", condition.Condition, upId);
            var healthCondition = await _healthConditionsRepository.GetAsync(hc => hc.HealthConditionName == condition.Condition);
            if (healthCondition == null)
                throw new Exception($"Health condition '{condition.Condition}' not found.");

            var existingCondition = await _personalHealthConditionsRepository.GetAsync(phc => phc.UPId == upId && phc.HealthConditionId == healthCondition.HealthConditionId);
            if (existingCondition != null)
            {
                _logger.LogInformation("Updating existing health condition: {Condition}", condition.Condition);
                existingCondition.Status = condition.Status;
                await _personalHealthConditionsRepository.UpdateAsync(existingCondition);
            }
            else
            {
                _logger.LogInformation("Adding new health condition: {Condition}", condition.Condition);
                var newCondition = new PersonalHealthConditions
                {
                    UPId = upId,
                    HealthConditionId = healthCondition.HealthConditionId,
                    Status = condition.Status
                };
                await _personalHealthConditionsRepository.AddAsync(newCondition);
            }
        }
    }
}