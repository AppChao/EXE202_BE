using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class SubcriptionUsers
{
    [Key]
    [Column(Order = 1)]
    [ForeignKey("UserProfiles")]
    public int UPId { get; set; }
    
    [Key]
    [Column(Order = 2)]
    [ForeignKey("Subcriptions")]
    public int SubcriptionId { get; set; }

    public virtual UserProfiles UserProfile { get; set; } =  null!;
    
    public virtual Subcriptions Subcription { get; set; } =  null!;
}