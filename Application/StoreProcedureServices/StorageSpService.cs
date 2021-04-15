using System.Threading.Tasks;
using Application.Seed;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Persistence.EFModels;
using Persistence.Stores;

namespace Application.StoreProcedureServices
{
    public class StorageSpService : IStorageSpService
    {
        private readonly IStorageStore _storageStore;
        private readonly ILogger _logger;
        private readonly ISeedService _seedService;
        private readonly AppDbContext _context;

        public StorageSpService(IStorageStore storageStore, ILogger logger, ISeedService seedService,
            AppDbContext context)
        {
            _storageStore = storageStore;
            _logger = logger;
            _seedService = seedService;
            _context = context;
        }

        public async Task<Result> AddStorages()
        {
            var storages = _seedService.GetStorageBulkData();

            _logger.Info("AddStoragesSp Started");

            var result = await _storageStore.AddStorages(storages);

            _logger.Info("AddStoragesSp Finished");

            return result;
        }



    }

    public interface IStorageSpService
    {
        Task<Result> AddStorages();
    }
}