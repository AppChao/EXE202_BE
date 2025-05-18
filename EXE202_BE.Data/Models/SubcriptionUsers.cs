using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class SubcriptionUsers
{ 
    public int UPId { get; set; }
    
    public int SubcriptionId { get; set; }

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } =  null!;
    
    [ForeignKey("SubcriptionId")]
    public virtual Subcriptions Subcription { get; set; } =  null!;
}