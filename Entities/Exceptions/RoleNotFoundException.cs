using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Entities.Exceptions
{
    public sealed class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(Guid roleId)
        : base($"The role with id: {roleId} doesn't exist in the database.")
        {
        }
    }
}
