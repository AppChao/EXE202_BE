using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

[Table("RecipeChunks")]
public class RecipeChunks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ChunkId { get; set; }

    public int RecipeId { get; set; }

    public string ChunkText { get; set; } = string.Empty;

    public string ChunkType { get; set; } = string.Empty; // e.g., "Steps", "Meals", etc.

    [Column(TypeName = "vector(3072)")]
    public float[] Embedding { get; set; } = new float[3072]; // Gemini embedding
    
    public string? RecipeName { get; set; } // For prompt context/debugging

    [ForeignKey("RecipeId")]
    public Recipes Recipe { get; set; } = null!;
}