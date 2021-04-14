using System;
using System.Collections.Generic;

namespace Persistence.EFModels
{
    public class Storage
    {
        public Storage()
        {
            Products = new HashSet<Product>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool Enabled { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
