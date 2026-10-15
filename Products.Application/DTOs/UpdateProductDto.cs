using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "1", "999999999")]
        public decimal Price { get; set; }
    }
}
