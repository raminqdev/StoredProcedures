using System.Threading.Tasks;
using Application.EFCoreServices;
using Application.StoreProcedureServices;
using AspNetCore.Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.EFModels;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageSpService _storageSpService;
        private readonly IStorageEfCoreService _storageEfCoreService;
        private readonly IStorageSqlCommandService _storageSqlCommandService;

        public StorageController(IStorageSpService storageSpService,
            IStorageEfCoreService storageEfCoreService,
            IStorageSqlCommandService storageSqlCommandService)
        {
            _storageSpService = storageSpService;
            _storageEfCoreService = storageEfCoreService;
            _storageSqlCommandService = storageSqlCommandService;
        }

        [HttpPost("AddStoragesSp")]
        public async Task<Result> AddStoragesSp()
        {
            return await _storageSpService.AddStorages();
        }
        
        [HttpPost("AddStoragesEfCore")]
        public async Task<Result> AddStoragesEfCore()
        {
            return await _storageEfCoreService.AddStorages();
        }
        
        
        [HttpPost("CreateOrUpdateSqlCommand")]
        public async Task<Result> CreateOrUpdateSqlCommand(Storage storage)
        {
            return await _storageSqlCommandService.CreateOrUpdate(storage);
        }
    }
}