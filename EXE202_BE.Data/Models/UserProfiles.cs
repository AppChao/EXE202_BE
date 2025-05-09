using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Data.Models;

public partial class UserProfiles
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UPId { get; set; }

    public string? FullName { get; set; } = string.Empty;

    public int? SubcriptionId { get; set; }
    
    [Required] public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")] 
    public virtual IdentityUser User { get; set; } = null!;

    public virtual Subcriptions Subcription { get; set; } = null!;
}