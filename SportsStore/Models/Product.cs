using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {
        public long ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Column attribute is used to specify the SQL data type
        // that will be used to store values for this property.
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
