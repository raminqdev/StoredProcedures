using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Persistence.EFModels;
using Persistence.ProcedureServices;

namespace Persistence.Stores
{
    public class ProductStore : IProductStore
    {
        private readonly IDbProcedureService _dbProcedureService;
        private readonly ILogger _logger;

        public ProductStore(IDbProcedureService dbProcedureService, ILogger logger)
        {
            _logger = logger;
            _dbProcedureService = dbProcedureService;
        }

        public async Task<Result> CreateOrUpdate(Product product)
        {
            try
            {
                var res = await _dbProcedureService.CreateOrUpdateProductAsync(
                    id: product.Id,
                    name: product.Name,
                    code: product.Code,
                    quantity: product.Quantity,
                    unitePrice: product.UnitePrice,
                    description: product.Description,
                    enabled: product.Enabled,
                    storageId: product.StorageId,
                    supplierId: product.SupplierId);

                return res.Succeed
                    ? Result.Successful()
                    : Result.Failed(Error.WithData(1000, new string[] { "" }));
            }
            catch (SqlException ex)
            {
                _logger.Exception(ex, data: product);
                return Result.Failed(Error.WithData(1000, new string[] { "Sql Exception" }));
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, data: product);
                return Result.Failed(Error.WithData(1000, new string[] { "Exception" }));
            }
        }

        public async Task<IList<Product>> List()
        {
            var procedureResult = await _dbProcedureService.GetAllProductsAsync();

            var res = procedureResult.DataSet.Tables[0].AsEnumerable().Select(p => new Product
            {
                Id = new Guid(Convert.ToString(p["Id"])!),
                Name = Convert.ToString(p["Name"]),
                Code = Convert.ToString(p["Code"]),
                Quantity = Convert.ToInt16(p["Quantity"]),
                UnitePrice = Convert.ToDecimal(p["UnitePrice"]),
                Description = Convert.ToString(p["Description"]),
                Enabled = Convert.ToBoolean(p["Enabled"])
            }).ToList();

            return res;
        }

        public async Task<IList<Product>> ConvertedList()
        {
            var procedureResult = await _dbProcedureService.GetAllProductsAsync();

            var res = procedureResult.List<Product>(0).ToList();

            return res;
        }

        public async Task<ResultList<Product>> ProductReport(int? maxQuantity, int? minQuantity,
            bool? enabled, decimal? maxPrice, decimal? minPrice, Guid? storageId, Guid? supplierId)
        {
            var result = await _dbProcedureService.GetProductReportAsync(
                maxQuantity: maxQuantity,
                minQuantity: minQuantity,
                enabled: enabled,
                maxPrice: maxPrice,
                minPrice: minPrice,
                storageId: storageId,
                supplierId: supplierId
            );

            var res = result.DataSet.Tables[0].AsEnumerable()
                .Select(g =>
                    {
                        var STOID = g["STOId"];
                        var SUPID = g["SUPId"];
                        return new Product
                        {
                            Id = new Guid(g["Id"].ToString()),
                            Code = g["Code"].ToString(),
                            Name = g["Name"].ToString(),
                            Quantity = Convert.ToInt16(g["Quantity"].ToString()),
                            UnitePrice = Convert.ToDecimal(g["UnitePrice"].ToString()),
                            Description = g["Description"].ToString(),
                            Enabled = Convert.ToBoolean(g["Enabled"].ToString()),
                            StorageId = STOID == DBNull.Value
                                ? null
                                : (Guid?)new Guid(STOID.ToString()!),
                            Storage = STOID == DBNull.Value
                                ? null
                                : new Storage
                                {
                                    Id = new Guid(STOID.ToString()!),
                                    Name = g["STOName"].ToString(),
                                    Phone = g["STOPhone"].ToString(),
                                    Enabled = Convert.ToBoolean(g["STOEnabled"].ToString())
                                },
                            SupplierId = SUPID == DBNull.Value
                                ? null
                                : (Guid?)new Guid(SUPID.ToString()!),
                            Supplier = SUPID == DBNull.Value
                                ? null
                                : new Supplier
                                {
                                    Id = new Guid(SUPID.ToString()!),
                                    CompanyName = g["CompanyName"].ToString(),
                                    ContactName = g["ContactName"].ToString(),
                                    Phone = g["SUPPhone"].ToString(),
                                    Address = g["Address"].ToString(),
                                    Enabled = Convert.ToBoolean(g["SUPEnabled"].ToString())
                                }
                        };
                    }
                ).OrderByDescending(p => p.UnitePrice).ToList();

            var count = res.Count;
            return new ResultList<Product>
            {
                Items = count > 0 ? res.GetRange(0, 2) : null,
                TotalCount = count
            };
        }
    }


    public interface IProductStore
    {
        Task<Result> CreateOrUpdate(Product product);

        Task<IList<Product>> List();

        Task<IList<Product>> ConvertedList();

        Task<ResultList<Product>> ProductReport(int? maxQuantity, int? minQuantity,
            bool? enabled, decimal? maxPrice, decimal? minPrice, Guid? storageId, Guid? supplierId);
    }
}