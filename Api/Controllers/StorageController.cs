using System;
using System.Threading.Tasks;
using Application.EFCoreServices;
using Application.StoreProcedureServices;
using AspNetCore.Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.EFModels;
using Persistence.Generator.Helpers;

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

        [HttpPost("AddBulkStoragesSp")]
        public async Task<Result> AddBulkStoragesSp()
        {
            return await _storageSpService.AddStorages();
        }
        
        [HttpPost("AddStoragesEfCore")]
        public async Task<Result> AddStoragesEfCore()
        {
            return await _storageEfCoreService.AddStorages();
        }
        
        
        [HttpPost("CreateOrUpdateSqlCommand")]
        public async Task<Result<Guid>> CreateOrUpdateSqlCommand(Storage storage)
        {
            return await _storageSqlCommandService.CreateOrUpdate(storage);
        }
    }
}