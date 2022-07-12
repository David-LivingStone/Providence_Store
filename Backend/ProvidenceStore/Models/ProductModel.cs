using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvidenceStore.Models
{
    public class ProductModel
    {
        public Guid id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Date { get; set; }
    }
}
