using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Shared.DataTransferObjects
{
    public abstract record UserForManipulationDTO {
        [Required(ErrorMessage = "User name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "User must have email")]
        [MaxLength(256, ErrorMessage = "Maximum length for the Email is 256 characters.")]
        [EmailAddress]
        public string? Email { get; init; }
        [Required(ErrorMessage = "User requires a password")]
        [MinLength(8, ErrorMessage = "Minimal length for your password is 8 characters")]
        public string? Password { get; init; }
    }
}
