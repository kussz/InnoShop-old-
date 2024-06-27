using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Shared.DataTransferObjects
{
    public abstract record RoleForManipulationDTO
    {
        [Required(ErrorMessage = "Role name is a required field")]
        public string Name { get; init; }
        public bool ManipulationAccess { get; init; } 
        public bool PostAccess {  get; init; } 
        public IEnumerable<UserForRegDTO> Users { get; init; }
    }
}
