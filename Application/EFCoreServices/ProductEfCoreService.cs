using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Microsoft.Data.SqlClient;
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

        public async Task<IList<Product>> ListProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public ResultList<Product> ProductReport(ProductReportRequestModel model)
        {
            /* 
                In EntityFrameworkCore we have two methods for executing Stored Procedures that:
                1- Query for records from a database - FromSqlRaw()
                2- Execute a command (typically DML) on the database - ExecuteSqlRaw() or the awaitable ExecuteSqlRawAsync()
                https://referbruv.com/blog/posts/working-with-stored-procedures-in-aspnet-core-ef-core
             */

            var maxQuantityParam = new SqlParameter("@MaxQuantity", model.MaxQuantity);
            var minQuantityParam = new SqlParameter("@MinQuantity", model.MinQuantity);
            var enabledParam = new SqlParameter("@Enabled", model.Enabled);
            var maxPriceParam = new SqlParameter("@MaxPrice", model.MaxPrice);
            var minPriceParam = new SqlParameter("@MinPrice", model.MinPrice);
            var storageIdParam = new SqlParameter("@StorageId", model.StorageId == null ? DBNull.Value : model.StorageId);
            var supplierIdParam = new SqlParameter("@SupplierId", model.SupplierId == null ? DBNull.Value : model.SupplierId);

            var result = _context.Products.FromSqlRaw("exec spGetProductReport @MaxQuantity, @MinQuantity, @Enabled, @MaxPrice, @MinPrice, @StorageId, @SupplierId",
                            maxQuantityParam, minQuantityParam, enabledParam, maxPriceParam,
                            minPriceParam, storageIdParam, supplierIdParam).ToList();
            //The required column 'StorageId' was not present in the results of a 'FromSql' operation.
            return ResultList<Product>.Successful(null);
        }
    }

    public interface IProductEfCoreService
    {
        Task<IList<Product>> ListProducts();
        ResultList<Product> ProductReport(ProductReportRequestModel model);
    }
}