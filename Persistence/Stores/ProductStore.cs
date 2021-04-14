using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Lib.Models;
using Microsoft.Data.SqlClient;
using Persistence.EFModels;

namespace Persistence.Stores
{
    public class ProductStore : IProductStore
    {
        private readonly IDbProcedureService _dbProcedureService;

        public ProductStore(IDbProcedureService dbProcedureService)
        {
            this._dbProcedureService = dbProcedureService;
        }

        public async Task<Result> CreateOrUpdate(Product product)
        {
            try
            {
                var res = await _dbProcedureService.CreateOrUpdateProductAsync(
                    id: product.Id,
                    name: product.Name,
                    code:product.Code,
                    quantity:product.Quantity,
                    unitePrice:product.UnitePrice,
                    description:product.Description,
                    enabled: product.Enabled,
                    storageId:product.StorageId);

                return res.Succeed
                    ? Result.Successful()
                    : Result.Failed(Error.WithData(1000,new string[]{""}));
            }
            catch (SqlException ex)
            {
                return Result.Failed(Error.WithData(1000,new string[]{"Sql Exception"}));
            }
            catch (Exception ex)
            {
                return Result.Failed(Error.WithData(1000,new string[]{"Exception"}));
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

    }


    public interface IProductStore
    {
        Task<Result> CreateOrUpdate(Product product);
        
        Task<IList<Product>> List();

        Task<IList<Product>> ConvertedList();
    }
}
