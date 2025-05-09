using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Recipes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecipeId { get; set; }

    public string Nation { get; set; }

    public string RecipeSteps { get; set; }

    public string InstructionVideoLink { get; set; }

    public string RecipeName { get; set; }

    //Minutes
    public int TimeEstimation  { get; set; }
    
    [Range(1, 10, ErrorMessage = "The difficulty rating should be between 1 and 10")]
    public int DifficultyEstimation { get; set; }
}