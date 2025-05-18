using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Data.Models;

public partial class ModifyIdentityUser : IdentityUser<int>
{
    public string? Status { get; set; }
}