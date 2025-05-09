using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Subcriptions
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubcriptionId { get; set; }

    public string? SubcriptionName { get; set; } = string.Empty;

    public string? SubcriptionInfor { get; set; } = string.Empty;
}