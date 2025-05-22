using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Recipes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecipeId { get; set; }

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

    [ForeignKey("CuisineId")]
    public virtual Cuisines Cuisine { get; set; } = null!;
    
    public virtual ICollection<Servings>? Servings {get; set;} = null!;

    public virtual ICollection<RecipeMealTypes>? RecipeMealTypes { get; set; } = null!;

    public virtual ICollection<RecipeHealthTags>? RecipeHealthTags { get; set; } =  null!;
}