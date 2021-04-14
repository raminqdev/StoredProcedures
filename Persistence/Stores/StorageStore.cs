using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Lib.Extensions;
using AspNetCore.Lib.Models;
using Persistence.EFModels;

namespace Persistence.Stores
{
    public class StorageStore : IStorageStore
    {
        private readonly IDbProcedureService _dbProcedureService;

        public StorageStore(IDbProcedureService dbProcedureService)
        {
            _dbProcedureService = dbProcedureService;
        }

        public async Task<Result> AddStorages(IEnumerable<Storage> storages)
        {
            if (storages == null || !storages.Any())
                return Result.Successful();

            var tables = storages.AsDataTables();

            var storageDataTables = tables.FirstOrDefault(t => t.TableName == "Storage");
            var productDataTables = tables.FirstOrDefault(t => t.TableName == "Product");
            
            var supplierDataTables = tables.FirstOrDefault(t => t.TableName == "Supplier") ?? storages
                .SelectMany(s => s.Products.Select(p => p.Supplier))
                .GroupBy(x => x.Id).Select(x => x.First())
                .AsDataTable();

            await _dbProcedureService.AddStorages_Suppliers_ProductsAsync(
                storageDataTables,
                supplierDataTables,
                productDataTables);

            return Result.Successful();
        }
    }

    public interface IStorageStore
    {
        Task<Result> AddStorages(IEnumerable<Storage> trips);
    }
}