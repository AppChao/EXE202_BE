namespace EXE202_BE.Data.DTOS.Ingredient;

public class CommonAllergenResponse
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; } = string.Empty;
    public string DefaultUnit { get; set; } = string.Empty;
}