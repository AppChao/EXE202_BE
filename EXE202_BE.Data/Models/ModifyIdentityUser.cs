using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Data.Models;

public partial class ModifyIdentityUser : IdentityUser
{
    public string? Status { get; set; }
}