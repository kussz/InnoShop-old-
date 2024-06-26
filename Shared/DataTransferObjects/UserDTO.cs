using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Shared.DataTransferObjects;

public record UserDTO(Guid Id, string Name, string Email, Guid RoleID, string PasswordHash, bool EmailConfirmed)
{
}
