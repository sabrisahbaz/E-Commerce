using E_Commerce.Data.Models.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Models.Entities
{
    public class Product : BaseEntity
    {
        [Required]

        public string Name { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int MyProperty { get; set; }
        public Category Category { get; set; }
        public byte[] Image { get; set; }
        public int Stock { get; set; }
        [Range(0,1000000)]
        public decimal Price { get; set; }
        public bool IsHome { get; set; }
        public bool IsApproved { get; set; }
    }
}




    
