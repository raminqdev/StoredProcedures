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
            var storageDataTables = tables.FirstOrDefault(t => t.TableName == "Storages");
            var productDataTables = tables.FirstOrDefault(t => t.TableName == "Products");
            await _dbProcedureService.AddStoragesAsync(storageDataTables, productDataTables);
            return Result.Successful();
        }
    }

    public interface IStorageStore
    {
        Task<Result> AddStorages(IEnumerable<Storage> trips);
    }
}