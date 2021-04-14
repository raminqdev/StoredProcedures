using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.EFModels;
using Persistence.Stores;

namespace Application.StoreProcedureServices.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IProductStore _productStore;
        private readonly ILogger _logger;

        public ProductService(AppDbContext context, IProductStore productStore, ILogger logger)
        {
            _context = context;
            _productStore = productStore;
            _logger = logger;
        }

        public async Task<IList<Persistence.EFModels.Product>> ListProductsEfCore()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Result> CreateOrUpdateSp(Persistence.EFModels.Product product)
        {
            _logger.Info("CreateOrUpdate");
            return await _productStore.CreateOrUpdate(product);
        }
        
        
    }

    public interface IProductService
    {
        Task<IList<Persistence.EFModels.Product>> ListProductsEfCore();

        Task<Result> CreateOrUpdateSp(Persistence.EFModels.Product product);
    }
}