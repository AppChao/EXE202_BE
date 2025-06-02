using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Cuisines
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CuisineId { get; set; }

    public string? Nation { get; set; } = string.Empty;

    public string? Region { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public virtual ICollection<Recipes> Recipes { get; set; } =  null!;
}