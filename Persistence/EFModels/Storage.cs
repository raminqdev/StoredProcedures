using System.Collections.Generic;

namespace DataAccess.EFModels
{
    public class Storage
    {
        public Storage()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool Enabled { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
