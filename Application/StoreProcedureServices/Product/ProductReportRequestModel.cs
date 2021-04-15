using System;

namespace Application.StoreProcedureServices.Product
{
    public class ProductReportRequestModel
    {
        public int? MaxQuantity { get; set; }
        public int? MinQuantity { get; set; }
        public bool? Enabled { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public Guid? StorageId { get; set; }
        public Guid? SupplierId { get; set; }
    }
}