namespace EXE202_BE.Data.DTOS.Ingredient;

public class IngredientTypeResponse
{
    public int IngredientTypeId { get; set; }
    public string TypeName { get; set; } = string.Empty;
}

public class IngredientResponse
{
    public int IngredientId { get; set; }
    public string IngredientName { get; set; } = string.Empty;
    public string DefaultUnit { get; set; } = string.Empty;
}