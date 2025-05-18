using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Data.Models;

public partial class Devices
{
    [Key]
    public int DeviceToken { get; set; }
    
    [Required] public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")] 
    public virtual IdentityUser User { get; set; } = null!;

    public string? Platform { get; set; } =  string.Empty;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}