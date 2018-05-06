using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class Store
    {
        public int StoreId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual ICollection<ProductSold> ProductSolds { get; set; }
    }
}