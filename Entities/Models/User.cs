using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMS.Entities.Models;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = "";
}
