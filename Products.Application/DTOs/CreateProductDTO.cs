using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "999999999")]
        public decimal Price { get; set; }
    }
}
