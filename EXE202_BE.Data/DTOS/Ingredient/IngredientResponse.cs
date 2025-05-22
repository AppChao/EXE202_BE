namespace EXE202_BE.Data.DTOS.Ingredient;

public class IngredientResponse
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; }
    
    public string DefaultUnit { get; set; }
}