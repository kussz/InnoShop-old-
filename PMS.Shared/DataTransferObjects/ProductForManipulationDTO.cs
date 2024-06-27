using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Shared.DataTransferObjects
{
    public abstract record ProductForManipulationDTO
    {

        [Required(ErrorMessage = "Product must have a name")]
        [MaxLength(100, ErrorMessage = "MaxLength for name = 100")]
        public string Name { get; set; }
        public string Description { get; set; } = "";
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative")]
        public decimal? Price { get; set; }
        public bool Visibility { get; set; } //0 - private, 1 - public
    }


}
