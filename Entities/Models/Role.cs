using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMS.Entities.Models;

public class Role
{
    [Column("RoleID")]
    [Key]
    public Guid Id { get; set; }
    [Column("RoleName")]
    [Required(ErrorMessage ="Name is required for role")]
    public string Name { get; set; }
    [Column("ManipulationAccess")]
    public bool ManipulationAccess { get; set; }
    [Column("PostAccess")]
    public bool PostAccess {  get; set; }

}
