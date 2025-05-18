using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Recipes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecipeId { get; set; }

    [ForeignKey("Cuisines")]
    public int? CuisineId { get; set; }

    public string? Meals { get; set; } =  string.Empty;

    public string? RecipeSteps { get; set; } =  string.Empty;

    public string? InstructionVideoLink { get; set; }  =  string.Empty;

    public string? RecipeName { get; set; }  =  string.Empty;

    //Minutes
    public int TimeEstimation  { get; set; }
    
    [Range(1, 10, ErrorMessage = "The difficulty rating should be between 1 and 10")]
    public int DifficultyEstimation { get; set; }

    public int? DefaultServing { get; set; }

    public virtual Cuisines Cuisine { get; set; } = null!;
}