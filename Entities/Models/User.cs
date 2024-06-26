using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMS.Entities.Models;

public class User
{
    [Column("UserID")]
    [Key]
    public Guid Id { get; set; } = new();
    [Required(ErrorMessage = "User name is a required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    [Column("Name")]
    public string Name { get; set; } = "";
    [Required(ErrorMessage = "User must have email")]
    [MaxLength(256, ErrorMessage = "Maximum length for the Email is 256 characters.")]
    [EmailAddress]
    [Column("Email")]
    public string Email { get; set; } = "";
    [ForeignKey(nameof(Role))]
    [Column("Role")]
    public Guid RoleID { get; set; } = new();
    [Column("PasswordHash")]
    public string PasswordHash { get; set; } = "";
    [Column("Confirmation")]
    public bool EmailConfirmed { get; set; } = false;
}
