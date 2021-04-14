namespace Application.StoreProcedureServices.Product
{
    public class ProductReportRequestModel
    {
        public int? MaxQuantity { get; set; }
        public int? MinQuantity { get; set; }
        public bool? Enabled { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public int? StorageId { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
    }
}