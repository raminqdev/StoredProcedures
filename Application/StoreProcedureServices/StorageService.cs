using System.Threading.Tasks;
using Application.Seed;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Persistence.Stores;

namespace Application.StoreProcedureServices
{
    public class StorageService : IStorageService
    {
        private readonly IStorageStore _storageStore;
        private readonly ILogger _logger;
        private readonly ISeedService _seedService;

        public StorageService(IStorageStore storageStore, ILogger logger, ISeedService seedService)
        {
            _storageStore = storageStore;
            _logger = logger;
            _seedService = seedService;
        }

        public async Task<Result> AddStorages()
        {
            var storages = _seedService.GetStorageBulkData();

            _logger.Info("AddStorages Started");

            var result = await _storageStore.AddStorages(storages);

            _logger.Info("AddStorages Finished");

            return result;
        }
    }

    public interface IStorageService
    {
        Task<Result> AddStorages();
    }
}