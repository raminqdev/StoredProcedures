using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.EFModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(8, 2)")]
        public decimal UnitePrice { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public Guid? StorageId { get; set; }
        public Storage Storage { get; set; }
        
        public Guid? SupplierId { get; set; }
        
        public Supplier Supplier { get; set; }
    }
}
