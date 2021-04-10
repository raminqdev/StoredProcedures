using DataAccess.EFModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Stores
{
    public class ProductStore : IProductStore
    {
        private readonly IDbProcedureService dbProcedureService;

        public ProductStore(IDbProcedureService dbProcedureService)
        {
            this.dbProcedureService = dbProcedureService;
        }

        public async Task<IList<Product>> List()
        {
            var procedureResult = await dbProcedureService.GetAllProductsAsync();

            var res = procedureResult.DataSet.Tables[0].AsEnumerable().Select(p => new Product
            {
                Id = Convert.ToInt32(p["Id"]),
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
            var procedureResult = await dbProcedureService.GetAllProductsAsync();

            var res = procedureResult.List<Product>(0).ToList();

            return res;
        }

    }


    public interface IProductStore
    {
        Task<IList<Product>> List();

        Task<IList<Product>> ConvertedList();
    }
}
