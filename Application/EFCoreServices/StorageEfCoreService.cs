using System.Threading.Tasks;
using Application.Seed;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Persistence.EFModels;

namespace Application.EFCoreServices
{
    public class StorageEfCoreService : IStorageEfCoreService
    {
        private readonly ILogger _logger;
        private readonly ISeedService _seedService;
        private readonly AppDbContext _context;

        public StorageEfCoreService(ILogger logger, ISeedService seedService,
            AppDbContext context)
        {
            _logger = logger;
            _seedService = seedService;
            _context = context;
        }


        public async Task<Result> AddStorages()
        {
            var storages = _seedService.GetStorageBulkData();


            _logger.Info("AddStoragesEfCore Started");

            _context.AddRange(storages.GetRange(0, 20));

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

    public interface IStorageEfCoreService
    {
        Task<Result> AddStorages();
    }
}