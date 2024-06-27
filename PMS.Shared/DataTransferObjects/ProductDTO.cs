using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Shared.DataTransferObjects
{
    public record ProductDTO(Guid Id, string Name, string Description, decimal Price, bool Visibility, Guid UserId, DateTime CreationDate );
}
