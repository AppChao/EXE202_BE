using EXE202_BE.Data.Models;

public class IngredientDto
{
    public string IngredientId { get; set; }
    public string IngredientName { get; set; }
    public string IngredientTypeId { get; set; }
    public string CaloriesPer100g { get; set; }
    public string DefaultUnit { get; set; }
    public string GramPerUnit { get; set; }
    
    public string? IconLibrary { get; set; } 

    public string? IconName { get; set; } 

    public Ingredients ToEntity()
    {
        return new Ingredients
        {
            IngredientId = int.Parse(IngredientId),
            IngredientName = IngredientName,
            IngredientTypeId = int.Parse(IngredientTypeId),
            CaloriesPer100g = double.Parse(CaloriesPer100g),
            DefaultUnit = DefaultUnit,
            GramPerUnit = double.Parse(GramPerUnit),
            IconLibrary = IconLibrary ?? null,
            IconName = IconName ?? null,
        };
    }
}