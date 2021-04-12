using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCore.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.EFModels;
using Persistence.Stores;

namespace Application.StoreProcedureServices
{
    public class ProductService:IProductService
    {
        private readonly AppDbContext _context;
        private readonly IProductStore _productStore;

        public ProductService(AppDbContext context,IProductStore productStore)
        {
            _context = context;
            _productStore = productStore;
        }

        public async Task<IList<Product>> ListProductsEntityFrameWorkCore()
        {
            return await _context.Products.ToListAsync();
        }
        
        public async Task<Result> CreateOrUpdate(Product product)
        {
            return await _productStore.CreateOrUpdate(product);
        }
        
    }

    public interface IProductService
    {
        Task<IList<Product>> ListProductsEntityFrameWorkCore();

        Task<Result> CreateOrUpdate(Product product);
    }
    
}