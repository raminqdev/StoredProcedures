using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Persistence.EFModels;
using Persistence.Stores;

namespace Application.StoreProcedureServices
{
    public class ProductSpService : IProductSpService
    {
        private readonly IProductStore _productStore;
        private readonly ILogger _logger;

        public ProductSpService( IProductStore productStore, ILogger logger)
        {
            _productStore = productStore;
            _logger = logger;
        }

        
        public async Task<IList<Product>> List()
        {
            return await _productStore.List();
        }
        
        public async Task<IList<Product>> ConvertedList()
        {
            return await _productStore.ConvertedList();
        }
        
        public async Task<Result> CreateOrUpdateSp(Persistence.EFModels.Product product)
        {
            _logger.Info("CreateOrUpdate");
            return await _productStore.CreateOrUpdate(product);
        }

        public async Task<ResultList<Persistence.EFModels.Product>> ProductReport(ProductReportRequestModel model)
        {
            _logger.Info("ProductReport Started");
            
            var result = await _productStore.ProductReport(model.MaxQuantity,  model.MinQuantity,
                model.Enabled, model.MaxPrice, model.MinPrice, model.StorageId, model.SupplierId);
            
            _logger.Info("ProductReport Finished");

            return result;
        }
        
    }

    public interface IProductSpService
    {
        Task<IList<Product>> List();
        Task<IList<Product>> ConvertedList();
        Task<Result> CreateOrUpdateSp(Persistence.EFModels.Product product);
        Task<ResultList<Persistence.EFModels.Product>> ProductReport(ProductReportRequestModel model);
    }
}