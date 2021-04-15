using System.Threading.Tasks;
using Application.EFCoreServices;
using Application.StoreProcedureServices;
using AspNetCore.Lib.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageSpService _storageSpService;
        private readonly IStorageEfCoreService _storageEfCoreService;

        public StorageController(IStorageSpService storageSpService,IStorageEfCoreService storageEfCoreService)
        {
            _storageSpService = storageSpService;
            _storageEfCoreService = storageEfCoreService;
        }

        [HttpPost]
        [Route("AddStoragesSp")]
        public async Task<Result> AddStoragesSp()
        {
            return await _storageSpService.AddStoragesSp();
        }
        
        [HttpPost]
        [Route("AddStoragesEfCore")]
        public async Task<Result> AddStoragesEfCore()
        {
            return await _storageEfCoreService.AddStoragesEf();
        }
    }
}