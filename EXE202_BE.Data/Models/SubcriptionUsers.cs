using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class SubcriptionUsers
{
    [ForeignKey("UserProfiles")]
    public int UPId { get; set; }
    
    [ForeignKey("Subcriptions")]
    public int SubcriptionId { get; set; }

    public virtual UserProfiles UserProfile { get; set; } =  null!;
    
    public virtual Subcriptions Subcription { get; set; } =  null!;
}