using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class Product
    {


        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int price { get; set; }

        public virtual ICollection<ProductSold> ProductSolds { get; set; }
    }
}