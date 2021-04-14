using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Seed;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Persistence.EFModels;
using Persistence.Stores;

namespace Application.StoreProcedureServices
{
    public class StorageService : IStorageService
    {
        private readonly IStorageStore _storageStore;
        private readonly ILogger _logger;
        private readonly ISeedService _seedService;
        private readonly AppDbContext _context;

        public StorageService(IStorageStore storageStore, ILogger logger, ISeedService seedService,
            AppDbContext context)
        {
            _storageStore = storageStore;
            _logger = logger;
            _seedService = seedService;
            _context = context;
        }

        public async Task<Result> AddStoragesSp()
        {
            var storages = _seedService.GetStorageBulkData();

            _logger.Info("AddStoragesSp Started");

            var result = await _storageStore.AddStorages(storages);

            _logger.Info("AddStoragesSp Finished");

            return result;
        }


        public async Task<Result> AddStoragesEfCore()
        {
            var storages = _seedService.GetStorageBulkData();
            

            _logger.Info("AddStoragesEfCore Started");

            _context.AddRange(storages.GetRange(0,20));
            
            // _context.AddRange(storages
            //     .SelectMany(s => s.Products.Select(p => p.Supplier))
            //     .GroupBy(x => x.Id).Select(x => x.First()));
            //
            // _context.AddRange(storages.SelectMany(s=>s.Products)
            //     .GroupBy(p=>p.Id).Select(g =>g.First()));
            
            await _context.SaveChangesAsync();

            _logger.Info("AddStoragesEfCore Finished");

            return Result.Successful();
        }
    }

    public interface IStorageService
    {
        Task<Result> AddStoragesSp();
        Task<Result> AddStoragesEfCore();
    }
}