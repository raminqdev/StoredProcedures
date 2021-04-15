using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.EFModels;

namespace Application.EFCoreServices
{
    public class ProductEfCoreService : IProductEfCoreService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public ProductEfCoreService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<Persistence.EFModels.Product>> ListProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<ResultList<Product>> ProductReport(ProductReportRequestModel model)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IProductEfCoreService
    {
        Task<IList<Persistence.EFModels.Product>> ListProducts();
        Task<ResultList<Persistence.EFModels.Product>> ProductReport(ProductReportRequestModel model);
    }
}